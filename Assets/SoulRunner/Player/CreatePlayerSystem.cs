using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player
{
  public class CreatePlayerSystem : IEcsInitSystem
  {
    private readonly EcsCustomInject<PlayerConfig> _playerCfg = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
      int player = _world.NewEntity();
      _world.Add<Movable>(player)
        .Speed = _playerCfg.Value.MoveSpeed;
      _world.Add<Jumpable>(player)
        .JumpForce = _playerCfg.Value.JumpForce;
      _world.Add<Kelli>(player)
        .NextHand = HandType.None;
      _world.Add<Fireable>(player)
        .Assign(x =>
        {
          x.FireDelay = _playerCfg.Value.FireDelay;
          x.FireTime = _playerCfg.Value.FireTime;
          return x;
        });
      _world.Add<Dashable>(player)
        .Assign(x =>
        {
          x.DashDelay = _playerCfg.Value.DashDelay;
          x.DashDuration = _playerCfg.Value.DashDuration;
          x.DashSpeed = _playerCfg.Value.DashSpeed;
          return x;
        });

      _world.Add<Crouchable>(player);
      _world.Add<ControllerByPlayer>(player);
    }
  }
}