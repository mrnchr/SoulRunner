using System;
using SoulRunner.DamageManagement;

namespace SoulRunner.Infrastructure
{
  public class DamageService : IDamageService
  {
    public Action<DamageMessage> OnDamaged;

    public virtual void ProcessDamage(View damaged, View damaging)
    {
    }

    public void InvokeOnDamaged(DamageMessage message)
    {
      OnDamaged?.Invoke(message);
    }
  }
}