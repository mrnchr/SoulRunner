using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public interface IHeroChar
  {
    public ObjectType Owner { get; set; }
  }
}