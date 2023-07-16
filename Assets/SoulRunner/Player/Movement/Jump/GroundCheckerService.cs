using Leopotam.EcsLite;
using SoulRunner.Utility.Ecs;
using Zenject;

namespace SoulRunner.Player
{
  public class GroundCheckerService
  {
    private EcsWorld _world;

    [Inject]
    public void Construct(EcsWorld world)
    {
      _world = world;
    }

    public void OnGroundEnter(int entity)
    {
      _world.Update<OnGround>(entity);
    }
  }
}