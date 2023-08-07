using System.Collections.Generic;
using System.Linq;

namespace SoulRunner.Characteristics
{
  public class CharMask<TChar>
    where TChar : ICharacteristic
  {
    protected IEnumerable<ICharacteristic> _chars;
    protected IEnumerable<TChar> _selected;
    
    public virtual TChar Char => Chars.First();
    public virtual List<TChar> Chars => new List<TChar>(_selected);

    public CharMask(IEnumerable<ICharacteristic> chars)
    {
      _chars = chars;
      _selected = chars.OfType<TChar>();
    }

    public virtual CharMask<TChar> With<T>()
    {
      _selected = _selected.Where(x => x is T);
      return this;
    }
  }
}