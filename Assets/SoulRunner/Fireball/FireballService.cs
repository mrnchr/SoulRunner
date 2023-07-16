using Leopotam.EcsLite;
using SoulRunner.Configuration.Source;
using SoulRunner.Player;
using SoulRunner.Utility.Ecs;
using Zenject;

namespace SoulRunner.Fireball
{
  public class FireballService
  {
    private EcsWorld _world;
    private FireballConfig _cfg;

    [Inject]
    public void Construct(EcsWorld world, FireballConfig fireballCfg)
    {
      _world = world;
      _cfg = fireballCfg;
    }

    public int Create()
    {
      int entity = _world.NewEntity();

      _world.Add<Fireball>(entity);
      _world.Add<Movable>(entity)
        .Speed = _cfg.Speed;

      return entity;
    }
  }
}