using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player
{
  public class CreatePlayerSystem : IEcsInitSystem
  {
    private readonly EcsCustomInject<Hero> _hero = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
      int player = _world.NewEntity();
      _world.Add<Movable>(player)
        .Speed = _hero.Value.MoveSpeed;
      _world.Add<Jumpable>(player)
        .jumpForce = _hero.Value.JumpForce;

      _world.Add<Crouchable>(player);
      _world.Add<ControllerByPlayer>(player);
    }
  }
}