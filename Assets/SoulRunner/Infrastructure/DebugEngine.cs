#if UNITY_EDITOR
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using SoulRunner.Utility.Ecs.Combine;

namespace SoulRunner.Infrastructure
{
  public class DebugEngine : IEcsEngine
  {
    public void Start(IEcsSystems systems)
    {
      systems
        .Add(new EcsWorldDebugSystem())
        .Add(new EcsSystemsDebugSystem());
    }
  }
}
#endif