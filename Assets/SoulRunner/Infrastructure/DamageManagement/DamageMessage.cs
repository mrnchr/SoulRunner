namespace SoulRunner.Infrastructure
{
  public struct DamageMessage
  {
    public View Damaged;
    public View Damaging;
    public float DamagePoint;

    public DamageMessage(View damaged, View damaging, float damagePoint)
    {
      Damaged = damaged;
      Damaging = damaging;
      DamagePoint = damagePoint;
    }
  }
}