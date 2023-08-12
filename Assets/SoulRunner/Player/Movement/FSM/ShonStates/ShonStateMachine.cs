using SoulRunner.Infrastructure;
using SoulRunner.Utility;

namespace SoulRunner.Player
{
  public class ShonStateMachine : PlayerStateMachine
  {
    protected override void Awake()
    {
      Owner = ObjectType.Shon;
      _states
        .AddItem(new PlayerEmptyState().Init(PlayerStateType.Empty))
        .AddItem(new PlayerStandState().Init(PlayerStateType.Stand))
        .AddItem(new ShonJumpState().Init(PlayerStateType.Jump))
        .AddItem(new ShonFallState().Init(PlayerStateType.Fall))
        .AddItem(new PlayerCrouchState().Init(PlayerStateType.Crouch))
        .AddItem(new PlayerClimbState().Init(PlayerStateType.Climb))
        .AddItem(new KelliDashState().Init(PlayerStateType.MainAbility))
        .AddItem(new ShonSwapState().Init(PlayerStateType.Swap));
      _moveState = new PlayerMoveState();
      _moveState.Init(PlayerStateType.Move);
      _swapState = new ShonSwapState();
      _swapState.Init(PlayerStateType.Swap);
      base.Awake();
    }
  }
}