using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player
{
  public class SpawnPlayerSystem : IEcsInitSystem
  {
    private readonly EcsCustomInject<IPlayerFactory> _factory = default;
    private readonly EcsCustomInject<Level.Level> _level = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
      int player = _world.Filter<ControllerByPlayer>().End(1).GetRawEntities()[0];

      _world.Add<PlayerViewRef>(player)
        .Value = _factory.Value.Create(_level.Value.PlayerSpawn.position);
    }
  }
}