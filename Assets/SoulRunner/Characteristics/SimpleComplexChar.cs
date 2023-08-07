using System;
using System.Collections.Generic;
using System.Linq;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class SimpleComplexChar<T> : Characteristic<T>
  {
    public delegate T CalculateHandler(SimpleComplexChar<T> characteristic, ref T value);
    
    protected List<CalculateHandler> _calculations = new List<CalculateHandler>();

    public virtual void AddCalculation(CalculateHandler item)
    {
      _calculations.Add(item);
      Calculate();
    }

    public virtual void RemoveCalculation(CalculateHandler item)
    {
      _calculations.Remove(item);
      Calculate();
    }

    public override void ToDefault()
    {
      _current = Default;
      Calculate();
    }

    protected virtual void Calculate()
    {
      Current = _calculations
        .Where(calculated => calculated != null)
        .Aggregate(Default, (current, calculation) => calculation(this, ref current));
    }

    protected override void SetDefault(T value)
    {
      _default = value;
      Calculate();
    }
  }
}