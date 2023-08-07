using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerFallAction : PlayerMovementAction, IFallAction, IUpdateAction
  {
    public PlayerFallAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Fall()
    {
      if (!IsActive || _variables.IsOnGround || _variables.IsFalling) return;
      if (_view.Rb.velocity.y >= 0) return;

      FallForcefully();
    }

    public virtual void FallForcefully()
    {
      if (_variables.IsJumping)
      {
        _variables.IsJumping = false;
        _variables.OnJumpEnd?.Invoke();
      }
      
      _variables.IsFalling = true;
      _variables.OnFallStart?.Invoke();
    }

    public virtual void Update()
    {
      Fall();
    }
  }
}