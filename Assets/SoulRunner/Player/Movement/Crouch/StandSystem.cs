using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public class StandSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<StandCommand, PlayerViewRef>> _crouching = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _crouching.Value)
      {
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        view.CrouchCollider.enabled = false;
        view.StayCollider.enabled = true;

        _world.Add<StartStand>(index);
      }
    }
  }
}