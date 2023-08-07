using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerClimbDownAction : PlayerMovementAction, IClimbDownAction, IStartAction
  {
    protected PlayerFallAction _fall;

    public PlayerClimbDownAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Start()
    {
      _fall = _machine.GetAction<PlayerFallAction>();
    }

    public virtual void ClimbDown()
    {
      if (!IsActive || !_variables.IsClimbing) return;
      
      _view.Rb.gravityScale = 1;
        
      _fall.FallForcefully();
      _variables.IsClimbing = false;
      _variables.OnClimbEnd?.Invoke();
    }
  }
}