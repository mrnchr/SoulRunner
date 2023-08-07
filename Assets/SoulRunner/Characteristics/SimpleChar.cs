using System;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class SimpleChar<T> : Characteristic<T>
  {
    protected override void SetDefault(T value)
    {
      base.SetDefault(value);
      ToDefault();
    }
  }
}