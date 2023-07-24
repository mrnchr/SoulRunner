using Leopotam.EcsLite;
using SoulRunner.Utility.Ecs;
using Zenject;

namespace SoulRunner.Player.Movement
{
  public class LedgeCheckerService
  {
    private EcsWorld _world;

    [Inject]
    public void Construct(EcsWorld world)
    {
      _world = world;
    }

    public void OnLedgeEnter(int entity, float height)
    {
      _world.AddSoftly<OnLedge>(entity)
        .PosX = height;
    }

    public void OnLedgeExit(int entity)
    {
      if (_world.Has<OnLedge>(entity))
        _world.Del<OnLedge>(entity);
    }
  }
}