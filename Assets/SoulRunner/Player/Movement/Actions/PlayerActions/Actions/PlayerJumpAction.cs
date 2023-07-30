using SoulRunner.Configuration;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerJumpAction : PlayerCycleAction, IJumpAction
  {
    protected readonly Rigidbody2D _rb;
    protected readonly PlayerConfig _playerCfg;

    public PlayerJumpAction(PlayerView view) : base(view)
    {
      _rb = view.Rb;
      _playerCfg = view.PlayerCfg;
    }

    public virtual void Jump()
    {
      if (!_variables.IsOnGround || _variables.IsCrouching || _variables.IsClimbing || _variables.IsFalling) return;
      
      JumpForcefully();
    }

    public virtual void JumpForcefully()
    {
      _rb.velocity = new Vector2(_rb.velocity.x, 0);
      _rb.AddForce(Vector2.up * _playerCfg.JumpForce, ForceMode2D.Impulse);

      _variables.IsOnGround = false;
      _variables.IsJumping = true;
      
      _variables.OnJumpStart?.Invoke();
    }
  }
}