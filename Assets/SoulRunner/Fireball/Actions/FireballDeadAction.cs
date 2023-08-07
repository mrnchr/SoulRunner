using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballDeadAction : MovementAction<FireballView>, IDeadAction, IUpdateAction
  {
    private readonly FireballActionVariables _variables;
    private readonly FireballSpec _spec;
    private Timer _beforeDeath = 0;

    public FireballDeadAction(ActionMachine<FireballView> machine) : base(machine)
    {
      _variables = _view.ActionVariables;
      _spec = _view.Spec;
    }

    public void Dead()
    {
      if (_variables.IsDying) return;

      _view.Rb.velocity = Vector2.zero;
      _variables.IsDying = true;
      _variables.OnDeathStart?.Invoke();
      TimerManager.AddTimer(_beforeDeath = _spec.DeathDuration);
    }

    public void Update()
    {
      if (_variables.IsDying && _beforeDeath <= 0)
      {
        Object.Destroy(_view.gameObject);
      }
    }
  }
}