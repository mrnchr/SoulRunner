using System;

namespace SoulRunner.Infrastructure
{
  [Obsolete]
  public class BaseDamageSystem : DamageSystem
  {
    public override bool CanProcess(ObjectType damaged, ObjectType damaging)
    {
      foreach (DamageRelation relation in Relations)
      {
        if (relation.Damaged.Contains(damaged) && relation.Damaging.Contains(damaging))
          return true;
        if (relation.IsInTurn && relation.Damaged.Contains(damaging) && relation.Damaging.Contains(damaged))
          return true;
      }

      return false;
    }

    public override DamageMessage ProcessDamage(View damaged, View damaging)
    {
      return new DamageMessage(damaged, damaging, 0);
    }
  }
}