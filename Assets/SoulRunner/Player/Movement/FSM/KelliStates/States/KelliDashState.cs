using SoulRunner.Control;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.FSM;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliDashState : PlayerState, IUpdateState
  {
    public override bool CanStart() => View.Chars.DashDelay.Current <= 0;

    public override void Start()
    {
      View.gameObject.layer = View.DashLayer.GetLayerIndex();
      View.Rb.gravityScale = 0;
      View.Rb.velocity = Vector2.right * (View.transform.localScale.x * View.Chars.DashSpeed);
      if (Machine.OldState == PlayerStateType.Fall)
      {
        Variables.IsFalling = false;
        Variables.OnFallEnd?.Invoke();
      }
      else if (Machine.OldState == PlayerStateType.Jump)
      {
        Variables.IsJumping = false;
        Variables.OnJumpEnd?.Invoke();
      }

      Variables.IsDashing = true;
      Variables.OnDashStart?.Invoke();
      View.Chars.DashDelay.ToDefault();
      TimerManager.AddTimer(Variables.DashHold = View.Spec.DashDuration);
      TimerManager.AddTimer(View.Chars.DashDelay.Current);
    }

    public override void End()
    {
      View.gameObject.layer = View.DefaultLayer.GetLayerIndex();
      View.Rb.gravityScale = 1;
      View.Rb.velocity = Vector2.zero;

      Variables.IsDashing = false;
      Variables.OnDashEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (Variables.IsOnLedge)
        Machine.ChangeState(PlayerStateType.Climb);
    }

    public void Update()
    {
      if (Variables.DashHold <= 0)
        Machine
          .ChangeState(!Variables.IsOnGround
            ? PlayerStateType.Fall
            : PlayerStateType.Stand);
    }
  }
}