using System;
using SoulRunner.Infrastructure.Actions;
using SoulRunner.Utility;

namespace SoulRunner.Fireball
{
  public class FireballActionMachine : ActionMachine<FireballView>
  {
    private FireballActionVariables _variables;
    private IDeadAction _dead;

    private void Awake()
    {
      _variables = View.ActionVariables;
      _actions
        .AddItem(new FireballDeadAction(this));

      _dead = GetAction<IDeadAction>();
    }

    private void OnEnable()
    {
      View.ObstacleChecker.OnCollided += _dead.Dead;
    }
    
    private void OnDisable()
    {
      View.ObstacleChecker.OnCollided -= _dead.Dead;
    }
  }
}