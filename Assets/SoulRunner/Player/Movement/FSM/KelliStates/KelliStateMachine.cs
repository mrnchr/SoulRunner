using SoulRunner.Control;
using SoulRunner.Infrastructure;
using SoulRunner.Utility;

namespace SoulRunner.Player
{
  public class KelliStateMachine : PlayerStateMachine
  {
    private PlayerState _fireState;
    
    protected override void Awake()
    {
      Owner = ObjectType.Kelli;
      _states
        .AddItem(new PlayerEmptyState().Init(PlayerStateType.Empty))
        .AddItem(new KelliStandState().Init(PlayerStateType.Stand))
        .AddItem(new KelliJumpState().Init(PlayerStateType.Jump))
        .AddItem(new KelliFallState().Init(PlayerStateType.Fall))
        .AddItem(new PlayerCrouchState().Init(PlayerStateType.Crouch))
        .AddItem(new KelliDashState().Init(PlayerStateType.MainAbility))
        .AddItem(new PlayerClimbState().Init(PlayerStateType.Climb))
        .AddItem(new KelliAttackInJumpState().Init(PlayerStateType.SideAbility))
        .AddItem(new KelliSuperAttackState().Init(PlayerStateType.SuperAttack))
        .AddItem(new KelliSwapState().Init(PlayerStateType.Swap));
      _moveState = new KelliMoveState();
      _moveState.Init(PlayerStateType.Move);
      _swapState = new KelliSwapState();
      _swapState.Init(PlayerStateType.Swap);
      base.Awake();

      _fireState = new KelliFireState();
      _fireState.Init(PlayerStateType.Fire);
      InitState(_fireState);

    }

    protected override void Start()
    {
      base.Start();
      _fireState.OnStart();
    }

    protected override void OnInputRead(InputValues inputs)
    {
        base.OnInputRead(inputs);
        _fireState.ProcessInput(inputs);
    }

    protected override void Update()
    {
      base.Update();
      ((KelliFireState)_fireState).Update();
    }
  }
}