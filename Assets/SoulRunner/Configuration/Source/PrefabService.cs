using Zenject;

namespace SoulRunner.Configuration
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