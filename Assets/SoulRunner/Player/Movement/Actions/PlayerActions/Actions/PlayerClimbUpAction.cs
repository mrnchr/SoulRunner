using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerClimbUpAction : PlayerMovementAction, IClimbUpAction, IStartAction
  {
    protected readonly PlayerActionMachine _machine;
    protected PlayerJumpAction _jump;
    protected PlayerClimbAction _climb;

    public PlayerClimbUpAction(PlayerView view) : base(view)
    {
      _machine = _view.ActionMachine;
    }

    public virtual void Start()
    {
      _jump = _machine.GetAction<PlayerJumpAction>();
      _climb = _machine.GetAction<PlayerClimbAction>();
    }

    public virtual void ClimbUp()
    {
      if (!_variables.IsClimbing) return;
      
      _view.Rb.gravityScale = 1;
      _jump.JumpForcefully();
      
      _variables.IsClimbing = false;
      _variables.OnClimbEnd?.Invoke();
    }
  }
}