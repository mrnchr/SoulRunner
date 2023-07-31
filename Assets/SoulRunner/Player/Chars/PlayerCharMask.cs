using System.Collections.Generic;
using System.Linq;
using SoulRunner.Characteristics;

namespace SoulRunner.Player
{
  public class PlayerCharMask<TChar> : CharMask<TChar>
    where TChar : ICharacteristic
  {
    public PlayerCharMask(IEnumerable<TChar> chars) : base(chars)
    {
    }

    public override TChar Char
    {
      get
      {
        if (Chars.Count == 1)
          return base.Char;

        return _chars.OfType<HeroChar>().First().Current == HeroType.Kelli
          ? With<IKelliChar>().Chars.First()
          : With<IShonChar>().Chars.First();
      }
    }
  }
}