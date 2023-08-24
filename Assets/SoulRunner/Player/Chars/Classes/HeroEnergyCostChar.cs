using System;
using SoulRunner.Characteristics;
using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  [Serializable]
  public class HeroEnergyCostChar : SimpleChar<float>, IHeroChar
  {
    public ObjectType Owner { get; set; }
  }
}