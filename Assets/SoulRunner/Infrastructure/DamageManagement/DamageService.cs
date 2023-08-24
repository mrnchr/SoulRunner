using System;

namespace SoulRunner.Infrastructure
{
  public class DamageService : IDamageService
  {
    public Action<DamageMessage> OnDamaged;

    public void InvokeOnDamaged(DamageMessage message)
    {
      OnDamaged?.Invoke(message);
    }
  }
}