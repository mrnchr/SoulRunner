using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using SoulRunner.Control;
using SoulRunner.Player;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs.Combine;

namespace SoulRunner.Infrastructure
{
  public class MoveEngine : IEcsEngine
  {
    public void Start(IEcsSystems systems)
    {
      systems
        .DelHere<MoveCommand>()
        .DelHere<JumpCommand>()
        .DelHere<CrouchCommand>()
        .DelHere<StandCommand>()
        .DelHere<Moving>()
        .DelHere<Jumping>()
        .DelHere<Standing>()
        .Add(new InputSystem())
        .Add(new DuringCrouchSystem())
        .Add(new MoveSystem())
        .Add(new JumpSystem())
        .Add(new StandSystem())
        .Add(new CrouchSystem());
    }
  }
}