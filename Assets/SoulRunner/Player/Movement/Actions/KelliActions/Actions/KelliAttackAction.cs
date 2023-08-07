using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliAttackAction : PlayerMovementAction, IKelliAttack, IStartAction, IUpdateAction
  {
    public Timer AttackDuration = 0;

    public KelliAttackAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public void Start()
    {
    }

    public void PlanAttack()
    {
      if (!IsActive || _chars.KelliAttackDelay.Current > 0 || _variables.IsDashing || _variables.IsAttacking) return;
      if (_variables.IsJumping || _variables.IsFalling)
      {
        _variables.IsAttackInJump = true;
        _view.Rb.velocity = new Vector2(_view.Rb.velocity.x, -_spec.SpeedWhenAttack);
        _view.AttackCollider.enabled = true;
        _view.StayCollider.enabled = false;
        _variables.OnAttackInJumpStart?.Invoke();
      }
    }

    public void Attack()
    {
      if (IsActive && _variables.IsAttackInJump)
      {
        _view.Rb.velocity = Vector2.zero;
        _variables.IsAttackInJump = false;
        _view.KelliAttackTrigger.enabled = true;
        _variables.IsAttacking = true;
        _variables.OnAttackInJumpEnd?.Invoke();
        _variables.OnAttackStart?.Invoke();
        _chars.KelliAttackDelay.ToDefault();
        TimerManager.AddTimer(AttackDuration = _spec.KelliAttackDuration);
        TimerManager.AddTimer(_chars.KelliAttackDelay.Current);
      }
    }


    public void Update()
    {
      if (_variables.IsAttacking && AttackDuration <= 0)
      {
        _view.AttackCollider.enabled = false;
        _view.StayCollider.enabled = true;
        _variables.IsAttacking = false;
        _view.KelliAttackTrigger.enabled = false;
        _variables.OnAttackEnd?.Invoke();
      }
    }

    public override void Deactivate()
    {
      _variables.IsAttackInJump = false;
      _variables.IsAttacking = false;
      base.Deactivate();
    }
  }
}