using System.Collections.Generic;
using UnityEngine;

namespace SoulRunner.Infrastructure
{
  public sealed class TimerManager : MonoBehaviour
  {
    private static readonly List<ITimerable> _timers = new List<ITimerable>();

    public static void AddTimer(ITimerable elem) => _timers.Add(elem);
    public static void RemoveTimer(ITimerable elem) => _timers.Remove(elem);

    private void FixedUpdate()
    {
      foreach (ITimerable timer in _timers.ToArray())
      {
        timer.TimeLeft -= Time.fixedDeltaTime;
        if (timer.TimeLeft <= 0)
          RemoveTimer(timer);
      }
    }
  }
}