using System.Collections.Generic;
using Leopotam.EcsLite;

namespace SoulRunner.Utility.Ecs.Combine
{
  public class EcsCombine : IEcsCombine
  {
    private readonly EcsWorld _world;
    private readonly List<IEcsEngine> _engines = new List<IEcsEngine>();
    private EcsSystems _systems;

    public EcsCombine(EcsWorld world)
    {
      _world = world;
    }

    public virtual EcsCombine Add(IEcsEngine engine)
    {
      _engines.Add(engine);
      return this;
    }

    public virtual EcsCombine Combine()
    {
      _systems = new EcsSystems(_world);
      foreach (IEcsEngine engine in _engines)
        engine.Start(_systems);
      _systems.Init();

      return this;
    }

    public virtual void Run() => _systems?.Run();

    public virtual void Destroy()
    {
      if (_systems == null) return;

      _systems.Destroy();
      _systems = null;
    }
  }
}