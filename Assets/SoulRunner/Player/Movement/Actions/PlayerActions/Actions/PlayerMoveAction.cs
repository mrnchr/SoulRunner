using SoulRunner.Configuration;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerMoveAction : PlayerMovementAction, IMoveAction
  {
    protected readonly Rigidbody2D _rb;
    protected readonly PlayerConfig _playerCfg;
    
    public PlayerMoveAction(PlayerView view) : base(view)
    {
      _rb = view.Rb;
      _playerCfg = view.PlayerCfg;
    }
    
    public virtual void Move(float direction)
    {
      if (_variables.IsCrouching || _variables.IsDashing || _variables.IsClimbing) return;
      
      _view.transform.localScale = SetViewDirection(_view.transform.localScale, direction);
      _rb.velocity = new Vector2(_playerCfg.MoveSpeed * direction, _rb.velocity.y);
      _variables.OnMove?.Invoke(direction);
    }
    
    protected virtual Vector3 SetViewDirection(Vector3 scale, float moveDirection)
    {
      if (moveDirection * scale.x < 0)
        scale.x *= -1;
      return scale;
    }
  }
}