using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunner.Characteristics;
using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class PlayerCharMask<TChar> : CharMask<TChar>
    where TChar : ICharacteristic
  {
    public PlayerCharMask(IEnumerable<ICharacteristic> chars) : base(chars)
    {
    }

    public override TChar Char
    {
      get
      {
        if (Chars.Count == 1)
          return base.Char;

        ObjectType type = _chars.OfType<HeroChar>().Single();
        foreach (TChar characteristic in Chars)
        {
          if (characteristic is not IHeroChar hero) continue;
          if (hero.Owner == type) return characteristic;
        }

        throw new ArgumentNullException(nameof(TChar));
      }
    }
  }
}