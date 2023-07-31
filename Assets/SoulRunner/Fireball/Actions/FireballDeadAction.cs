using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballDeadAction : MovementAction<FireballView>, IDeadAction, IUpdateAction
  {
    private readonly FireballActionVariables _variables;

    public FireballDeadAction(ActionMachine<FireballView> machine) : base(machine)
    {
    }

    public void Dead()
    {
      if (_variables.IsDying) return;

      _view.Rb.velocity = Vector2.zero;
      _variables.IsDying = true;
      _variables.OnDeathStart?.Invoke();
      TimerManager.AddTimer(_variables.BeforeDeath = _view.FireballCfg.DeathDuration);
    }

    public void Update()
    {
      if (_variables.IsDying && _variables.BeforeDeath <= 0)
      {
        Object.Destroy(_view.gameObject);
      }
    }
  }
}