using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public class DuringCrouchSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<Crouching>, Exc<CrouchCommand>> _crouching = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _crouching.Value)
      {
        UnityEngine.Debug.Log("Stand");
        _world.Del<Crouching>(index);
        _world.Add<StandCommand>(index);
      }
    }
  }
}