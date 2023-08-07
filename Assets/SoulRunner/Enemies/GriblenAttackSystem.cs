using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Enemies
{
  public class GriblenAttackSystem : AttackSystem
  {
    [SerializeField] private View _view;
    [SerializeField] private GriblenChars _chars;

    public override float GetDamagePoints() => throw new System.NotImplementedException();

    public override float TakeDamage(View damaging, float damagePoints)
    {
      float realDamage = 0;
      if (damaging.Id == ObjectType.Player)
        realDamage = damagePoints;
      
      _chars.Health.Current -= realDamage;
      _damageSvc.InvokeOnDamaged(new DamageMessage(_view, damaging, realDamage));
      return realDamage;
    }
  }
}