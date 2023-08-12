using SoulRunner.Control;
using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliSuperAttackState : PlayerState
  {
    private Timer _attackDuration;
    
    public override bool CanStart() => true;

    public override void Start()
    {
      View.Rb.velocity = Vector2.zero;
      View.KelliAttackTrigger.enabled = true;
      Variables.IsAttacking = true;
      Variables.OnAttackStart?.Invoke();
      View.Chars.KelliAttackDelay.ToDefault();
      TimerManager.AddTimer(_attackDuration = View.Spec.KelliAttackDuration);
      TimerManager.AddTimer(View.Chars.KelliAttackDelay.Current);
    }

    public override void End()
    {
      View.AttackCollider.enabled = false;
      View.StayCollider.enabled = true;
      Variables.IsAttacking = false;
      View.KelliAttackTrigger.enabled = false;
      Variables.OnAttackEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (_attackDuration <= 0)
        Machine.ChangeState(PlayerStateType.Stand);
    }
  }
}