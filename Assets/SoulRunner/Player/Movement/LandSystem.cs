using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public class LandSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<OnGround>> _ground = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _ground.Value)
      {
        if (_world.Has<Jumping>(index))
        {
          _world.Add<EndJump>(index);
          _world.Del<Jumping>(index);
        }

        if (_world.Has<Falling>(index))
        {
          _world.Add<EndFall>(index);
          _world.Del<Falling>(index);
        }
      }
    }
  }
}