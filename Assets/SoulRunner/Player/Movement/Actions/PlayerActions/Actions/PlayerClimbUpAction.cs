using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerClimbUpAction : PlayerMovementAction, IClimbUpAction, IStartAction
  {
    protected PlayerJumpAction _jump;

    public PlayerClimbUpAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Start()
    {
      _jump = _machine.GetAction<PlayerJumpAction>();
    }

    public virtual void ClimbUp()
    {
      if (!IsActive || !_variables.IsClimbing) return;
      
      _view.Rb.gravityScale = 1;
      _jump.JumpForcefully();
      
      _variables.IsClimbing = false;
      _variables.OnClimbEnd?.Invoke();
    }
  }
}