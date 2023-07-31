using System;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class Characteristic<T> : ICharacteristic, IDefaultChar, IMaxChar
  {
    public T Default;
    public T Current;
    public T Max;

    public static implicit operator T(Characteristic<T> obj) => obj.Current;

    public virtual void ToDefault()
    {
      Max = Default;
      ToMax();
    }

    public virtual void ToMax()
    {
      Current = Max;
    }
  }
}