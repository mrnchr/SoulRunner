using System;

namespace SoulRunner.Infrastructure
{
  public class Timer : ITimerable
  {
    public float TimeLeft { get; set; }

    public static implicit operator float(Timer obj) => obj.TimeLeft;
    public static implicit operator Timer(float obj) => new Timer { TimeLeft = obj };
  }
}