using SoulRunner.Control;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerJumpState : PlayerState
  {
    public override bool CanStart() => true;

    public override void Start()
    {
      JumpForcefully();
    }

    public override void End()
    {
      Variables.IsJumping = false;
      Variables.OnJumpEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if(Variables.IsOnGroundEnter)
        Machine.ChangeState(PlayerStateType.Stand);
      else if (Variables.IsOnLedge && (Machine.OldState != PlayerStateType.Climb || View.Rb.velocity.y < 0))
        Machine.ChangeState(PlayerStateType.Climb);
    }
    
    public virtual void JumpForcefully()
    {
      View.Rb.velocity = new Vector2(View.Rb.velocity.x, 0);
      View.Rb.AddForce(Vector2.up * View.Chars.JumpForce, ForceMode2D.Impulse);

      Variables.IsJumping = true;
      Variables.OnJumpStart?.Invoke();
    }
  }
}