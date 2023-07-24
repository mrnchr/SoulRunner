using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public class FallSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<PlayerViewRef>, Exc<OnGround, Jumping, Falling>> _falling = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _falling.Value)
      {
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        if (view.Rb.velocity.y >= 0) continue;
        
        _world.Add<StartFall>(index);
        _world.Add<Falling>(index);
      }
    }
  }
}