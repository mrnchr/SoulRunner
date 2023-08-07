using SoulRunner.Configuration;
using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerMoveAction : PlayerMovementAction, IMoveAction
  {
    protected readonly Rigidbody2D _rb;
    
    public PlayerMoveAction(ActionMachine<PlayerView> machine) : base(machine)
    {
      _rb = _view.Rb;
    }
    
    public virtual void Move(float direction)
    {
      if (!IsActive || _variables.IsCrouching || _variables.IsDashing || _variables.IsClimbing) return;
      
      _view.transform.localScale = SetViewDirection(_view.transform.localScale, direction);
      _rb.velocity = new Vector2(_chars.MoveSpeed * direction, _rb.velocity.y);
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