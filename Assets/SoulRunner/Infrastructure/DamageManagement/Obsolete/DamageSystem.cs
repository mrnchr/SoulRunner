using System;
using System.Collections.Generic;

namespace SoulRunner.Infrastructure
{
  [Obsolete]
  public abstract class DamageSystem : IDamageSystem
  {
    public List<DamageRelation> Relations = new List<DamageRelation>();

    public virtual bool CanProcess(View damaged, View damaging) => CanProcess(damaged.Id, damaging.Id);
    public abstract bool CanProcess(ObjectType damaged, ObjectType damaging);
    public abstract DamageMessage ProcessDamage(View damaged, View damaging);
  }
}