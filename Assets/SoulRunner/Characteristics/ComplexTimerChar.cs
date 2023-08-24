using System;
using SoulRunner.Infrastructure;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class ComplexTimerChar : ContinuousComplexChar<Timer>
  {
    public ComplexTimerChar()
    {
      Min = 0;
      _default = 0;
      _current = 0;
      _max = 0;
    }
    
    public override void Init() => _current = 0;

    public override void ToDefault()
    {
      _current.TimeLeft = Default;
      Calculate();
    }

    public override void ToMax()
    {
      _current.TimeLeft = Max;
    }

    protected override void Calculate()
    {
      var max = new Timer { TimeLeft = Default };
      foreach (CalculateHandler calculation in _calculations)
        max = calculation(this, ref max);
      Max.TimeLeft = max;
    }

    protected override void SetCurrent(Timer value)
    {
      _current.TimeLeft = value;
      if (_current is not IComparable<Timer> current) return;
      if (current.CompareTo(Min) == -1)
        _current.TimeLeft = Min;
      if (current.CompareTo(Max) == 1)
        _current.TimeLeft = Max;
    }

    protected override void SetDefault(Timer value)
    {
      _default.TimeLeft = value;
      Calculate();
    }

    protected override void SetMax(Timer value)
    {
      _max.TimeLeft = value;
      SetCurrent(Current);
    }
  }
}