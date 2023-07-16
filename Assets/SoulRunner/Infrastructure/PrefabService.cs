using SoulRunner.Configuration;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class PrefabService
  {
    public PrefabData Prefabs { get; private set; }

    [Inject]
    public PrefabService(PrefabData prefabs)
    {
      Prefabs = prefabs;
    }
  }
}