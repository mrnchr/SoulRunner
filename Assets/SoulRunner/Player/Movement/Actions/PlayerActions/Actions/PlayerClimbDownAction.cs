using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerClimbDownAction : PlayerMovementAction, IClimbDownAction, IStartAction
  {
    protected readonly PlayerActionMachine _machine;
    protected PlayerFallAction _fall;

    public PlayerClimbDownAction(PlayerView view) : base(view)
    {
      _machine = view.ActionMachine;
    }

    public virtual void Start()
    {
      _fall = _machine.GetAction<PlayerFallAction>();
    }

    public virtual void ClimbDown()
    {
      if (!_variables.IsClimbing) return;
      
      _view.Rb.gravityScale = 1;
        
      _fall.FallForcefully();
      _variables.IsClimbing = false;
      _variables.OnClimbEnd?.Invoke();
    }
  }
}