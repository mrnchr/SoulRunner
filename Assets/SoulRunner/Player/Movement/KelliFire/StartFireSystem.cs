using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public class StartFireSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<FireCommand, PlayerViewRef, Kelli>, Exc<DelayFire, Dashing>> _fire = default;
    private readonly EcsCustomInject<HeroConfig> _hero = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _fire.Value)
      {
        ref Kelli kelli = ref _world.Get<Kelli>(index);
        HandType commandHand = _world.Get<FireCommand>(index).Hand;
        if (kelli.NextHand == HandType.None || kelli.NextHand == commandHand)
        {
          _world.Add<StartFire>(index)
            .Hand = commandHand;
          kelli.NextHand = GetNextHand(commandHand);

          _world.Add<DelayFire>(index)
            .TimeLeft = _hero.Value.FireDelay;

          _world.Add<FireTimer>(index)
            .Assign(x =>
            {
              x.Hand = commandHand;
              x.TimeLeft = _hero.Value.FireTime;
              return x;
            });
        }
      }
    }

    private HandType GetNextHand(HandType hand) =>
      hand switch
      {
        HandType.Left  => HandType.Right,
        HandType.Right => HandType.Left,
        _              => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
      };
  }
}