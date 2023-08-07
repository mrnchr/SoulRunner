using System;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class ContinuousChar<T> : Characteristic<T>
  {
    public T Min;

    protected override void SetCurrent(T value)
    {
      _current = value;
      if (Current is not IComparable<T> current) return;
      if (current.CompareTo(Min) == -1)
        _current = Min;
      if (current.CompareTo(Default) == 1)
        _current = Default;
    }
  }
}