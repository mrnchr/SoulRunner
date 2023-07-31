using System.Collections.Generic;
using System.Linq;

namespace SoulRunner.Characteristics
{
  public class CharMask<TChar>
    where TChar : ICharacteristic
  {
    protected IEnumerable<TChar> _chars;
    
    public virtual TChar Char => Chars.First();
    public virtual List<TChar> Chars => new List<TChar>(_chars);

    public CharMask(IEnumerable<TChar> chars)
    {
      _chars = chars;
    }

    public virtual CharMask<TChar> With<T>()
    {
      _chars = _chars.Where(x => x is T);
      return this;
    }
  }
}