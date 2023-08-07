using System;

namespace SoulRunner.Infrastructure
{
  [Obsolete]
  public interface IDamageSystem
  {
    public bool CanProcess(View damaged, View damaging);
    public bool CanProcess(ObjectType damaged, ObjectType damaging);

    public DamageMessage ProcessDamage(View damaged, View damaging);
  }
}