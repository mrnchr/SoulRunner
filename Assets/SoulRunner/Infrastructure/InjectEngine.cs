using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs.Combine;
using UnityEngine;

namespace SoulRunner.Infrastructure
{
  public class InjectEngine : IEcsEngine
  {
    private readonly object[] _objects;

    public InjectEngine(object[] objects)
    {
      _objects = objects;
    }

    public void Start(IEcsSystems systems)
    {
      systems
        .Inject(_objects);
    }
  }
}