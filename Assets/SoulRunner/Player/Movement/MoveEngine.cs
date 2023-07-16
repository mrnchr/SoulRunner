using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using SoulRunner.Control;
using SoulRunner.Utility.Ecs;
using SoulRunner.Utility.Ecs.Combine;

namespace SoulRunner.Player.Movement
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
        .DelHere<FireCommand>()
        .DelHere<Moving>()
        .DelHere<Jumping>()
        .DelHere<Standing>()
        .DelHere<Firing>()
        .Add(new InputSystem())
        .Add(new DuringCrouchSystem())
        .Add(new MoveSystem())
        .Add(new JumpSystem())
        .Add(new StandSystem())
        .Add(new CrouchSystem())
        .Add(new DelTimer<HoldFireTimer>())
        .Add(new Timer<HoldFireTimer>())
        .Add(new Timer<FireTimer>())
        .Add(new StartFireSystem())
        .Add(new FireSystem());
    }
  }
}