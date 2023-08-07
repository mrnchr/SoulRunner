namespace SoulRunner.Infrastructure
{
  public interface IDamageService
  {
    public void InvokeOnDamaged(DamageMessage message);
  }
}