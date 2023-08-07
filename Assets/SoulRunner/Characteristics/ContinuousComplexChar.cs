using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class ContinuousComplexChar<T> : Characteristic<T>, IMaxChar
  {
    public delegate T CalculateHandler(ContinuousComplexChar<T> characteristic, ref T value);
    
    public T Min;
    public T Max
    {
      get => _max;
      set => SetMax(value);
    }

    protected List<CalculateHandler> _calculations = new List<CalculateHandler>();
    [SerializeField] protected T _max;

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

    public virtual void ToMax()
    {
      _current = _max;
    }

    protected virtual void Calculate()
    {
      T max = Default;
      foreach (CalculateHandler calculation in _calculations)
      {
        max = calculation(this, ref max);
      }
      Max = max;
    }

    protected override void SetCurrent(T value)
    {
      _current = value;
      if (_current is not IComparable<T> current) return;
      if (current.CompareTo(Min) == -1)
        _current = Min;
      if (current.CompareTo(Max) == 1)
        _current = Max;
    }

    protected override void SetDefault(T value)
    {
      _default = value;
      Calculate();
    }

    protected virtual void SetMax(T value)
    {
      _max = value;
      SetCurrent(Current);
    }
  }
}