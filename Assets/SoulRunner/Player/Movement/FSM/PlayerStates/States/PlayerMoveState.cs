using SoulRunner.Control;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerMoveState : PlayerState
  {
    private Rigidbody2D _rb;

    public override void OnStart()
    {
      _rb = View.Rb;
      base.OnStart();
    }

    public override bool CanStart() => true;

    public override void Start()
    {
    }

    public override void End()
    {
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (!Machine.IsActive ||
        Machine.CurrentState is PlayerStateType.Crouch or PlayerStateType.MainAbility or PlayerStateType.Climb) return;

      float direction = inputs.MoveDirection;
      View.transform.localScale = SetViewDirection(View.transform.localScale, direction);
      _rb.velocity = new Vector2(View.Chars.MoveSpeed * direction, _rb.velocity.y);
      Variables.OnMove?.Invoke(direction);
    }
    
    protected virtual Vector3 SetViewDirection(Vector3 scale, float moveDirection)
    {
      if (moveDirection * scale.x < 0)
        scale.x *= -1;
      return scale;
    }
  }
}