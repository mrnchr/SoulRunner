using Leopotam.EcsLite;
using SoulRunner.Player;
using SoulRunner.Utility.Ecs.Combine;
using UnityEngine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class Combiner : MonoBehaviour
  {
    private EcsCombine _engines;
    private EcsCombineInjector _injector;
    private EcsWorld _world;

    [Inject]
    public void Construct(EcsWorld world, EcsCombineInjector injector)
    {
      _world = world;
      _injector = injector;
    }

    private void Start()
    {
      _engines = new EcsCombine(_world)
        .Add(new PlayerInitEngine())
        .Add(new MoveEngine())
        .Add(new AnimEngine())
#if UNITY_EDITOR
        .Add(new DebugEngine())
#endif
        .Add(new InjectEngine(_injector.injects.ToArray()))
        .Combine();
    }

    private void Update()
    {
      _engines.Run();
    }

    private void OnDestroy()
    {
      if (_engines == null) return;

      _engines.Destroy();
      _engines = null;
    }
  }
}