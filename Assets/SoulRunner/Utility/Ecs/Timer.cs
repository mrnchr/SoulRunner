using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SoulRunner.Utility.Ecs
{
  public class Timer<T> : IEcsRunSystem
    where T : struct, ITimerable
  {
    private readonly EcsFilterInject<Inc<T>> _timers = default;

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _timers.Value)
        _timers.Pools.Inc1.Get(index).TimeLeft -= Time.deltaTime;
    }
  }
}