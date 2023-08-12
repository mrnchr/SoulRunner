using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballDeadState : FireballState
  {
    private Timer _beforeDeath = 0;
    public override bool CanStart() => true;

    public override void Start()
    {
      View.Rb.velocity = Vector2.zero;
      Variables.IsDying = true;
      Variables.OnDeathStart?.Invoke();
      TimerManager.AddTimer(_beforeDeath = View.Spec.DeathDuration);
    }

    public override void End()
    {
    }

    public override void Update()
    {
      if (Variables.IsDying && _beforeDeath <= 0)
      {
        Object.Destroy(View.gameObject);
      }
    }
  }
}