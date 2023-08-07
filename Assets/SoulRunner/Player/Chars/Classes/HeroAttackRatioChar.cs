using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class HeroAttackRatioChar : AttackRatioChar, IHeroChar
  {
    public ObjectType Owner { get; set; }
  }

  public interface IHeroChar
  {
    public ObjectType Owner { get; set; }
  }
}