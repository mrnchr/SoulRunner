using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public class ClimbDownSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<DownCommand, Climbing, PlayerViewRef>> _down = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _down.Value)
      {
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        view.Rb.gravityScale = 1;
        _world.Del<Climbing>(index);
        _world.Add<EndClimb>(index);
      }
    }
  }
}