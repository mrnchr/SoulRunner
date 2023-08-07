using System;
using SoulRunner.Infrastructure;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class TimerChar : Characteristic<Timer>
  {
    public TimerChar()
    {
      _default = 0;
      _current = 0;
    }
    
    public override void Init() => _current = 0;
    public override void ToDefault()
    {
      _current.TimeLeft = _default;
    }

    protected override void SetCurrent(Timer value)
    {
      _current.TimeLeft = value;
    }

    protected override void SetDefault(Timer value)
    {
      _default.TimeLeft = value;
    }
  }
}