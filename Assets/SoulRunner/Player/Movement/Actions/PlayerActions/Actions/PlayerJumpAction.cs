using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerJumpAction : PlayerMovementAction, IJumpAction
  {
    protected readonly Rigidbody2D _rb;

    public PlayerJumpAction(ActionMachine<PlayerView> machine) : base(machine)
    {
      _rb = _view.Rb;
    }

    public virtual void Jump()
    {
      if (!IsActive) return;
      if (!_variables.IsOnGround || _variables.IsJumping || _variables.IsCrouching || _variables.IsClimbing ||
        _variables.IsFalling || _variables.IsDashing) return;
      
      JumpForcefully();
    }

    public virtual void JumpForcefully()
    {
      _rb.velocity = new Vector2(_rb.velocity.x, 0);
      _rb.AddForce(Vector2.up * _chars.JumpForce, ForceMode2D.Impulse);

      _variables.IsJumping = true;
      
      _variables.OnJumpStart?.Invoke();
    }
  }
}