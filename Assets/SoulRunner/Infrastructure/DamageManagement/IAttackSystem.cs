namespace SoulRunner.Infrastructure
{
  public interface IAttackSystem
  {
    public float GetDamagePoints();
    public float TakeDamage(View damaging, float damagePoints);
  }
}