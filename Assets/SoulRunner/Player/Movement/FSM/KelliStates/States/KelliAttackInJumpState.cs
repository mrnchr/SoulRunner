using SoulRunner.Control;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliAttackInJumpState : PlayerState
  {
    public override bool CanStart()
    {
      return View.Chars.KelliAttackDelay.Current <= 0;
    }

    public override void Start()
    {
      Variables.IsAttackInJump = true;
      View.Rb.velocity = new Vector2(View.Rb.velocity.x, -View.Spec.SpeedWhenAttack);
      View.AttackCollider.enabled = true;
      View.StayCollider.enabled = false;
      Variables.OnAttackInJumpStart?.Invoke();
    }

    public override void End()
    {
      Variables.IsAttackInJump = false;
      Variables.OnAttackInJumpEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (Variables.IsOnGroundEnter)
        Machine.ChangeState(PlayerStateType.SuperAttack);
    }
  }
}