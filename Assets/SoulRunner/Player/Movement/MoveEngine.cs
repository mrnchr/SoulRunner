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
        .DelHere<UpCommand>()
        .DelHere<DownCommand>()
        .DelHere<StandCommand>()
        .DelHere<FireCommand>()
        .DelHere<DashCommand>()
        .DelHere<CatchCommand>()
        .DelHere<StartMove>()
        .DelHere<StartJump>()
        .DelHere<StartStand>()
        .DelHere<StartFire>()
        .DelHere<StartDash>()
        .DelHere<StartClimb>()
        .DelHere<StartFall>()
        .DelHere<EndJump>()
        .DelHere<EndClimb>()
        .DelHere<EndFall>()
        .Add(new InputSystem())
        // Kelli's Actions
        .Add(new DuringCrouchSystem())
        .Add(new MoveSystem())
        .Add(new JumpSystem())
        .Add(new LandSystem())
        .Add(new FallSystem())
        .Add(new StandSystem())
        .Add(new CrouchSystem())
        .Add(new DelTimer<DelayFire>())
        .Add(new Timer<DelayFire>())
        .Add(new Timer<FireTimer>())
        .Add(new StartFireSystem())
        .Add(new FireSystem())
        .Add(new DelTimer<DelayDash>())
        .Add(new Timer<DelayDash>())
        .Add(new Timer<Dashing>())
        .Add(new DashSystem())
        .Add(new EndDashSystem())
        // Kelli's Climb Actions
        .Add(new StartClimbSystem())
        .Add(new DelTimer<AfterJump>())
        .Add(new Timer<AfterJump>())
        .Add(new ClimbUpSystem())
        .Add(new ClimbDownSystem());
    }
  }
}