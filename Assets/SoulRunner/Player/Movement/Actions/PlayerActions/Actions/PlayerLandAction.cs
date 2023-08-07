using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerLandAction : PlayerMovementAction, ILandAction
  {
    public PlayerLandAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Land()
    {
      if (!IsActive) return;
      if (_variables.IsJumping)
      {
        _variables.IsJumping = false;
        _variables.OnJumpEnd?.Invoke();
      }

      if (_variables.IsFalling)
      {
        _variables.IsFalling = false;
        _variables.OnFallEnd?.Invoke();
      }
    }
  }
}