using SoulRunner.Infrastructure;
using UnityEngine;
using Zenject;

namespace SoulRunner.Fireball
{
  public class FireballFactory : IFireballFactory
  {
    private const float _atY = 1.2856f; // fireball animation center is higher zero on this number (ad hoc!!!)

    private readonly DiContainer _container;
    private readonly FireballView _prefab;

    [Inject]
    public FireballFactory(DiContainer container, PrefabService prefabSvc)
    {
      _container = container;
      _prefab = prefabSvc.Prefabs.Fireball;
    }
    
    public FireballView Create(Vector3 at)
    {
      at.y -= _atY;
      return _container.InstantiatePrefabForComponent<FireballView>(_prefab, at, Quaternion.identity, null);
    }
  }

  public interface IFireballFactory : IFactory<Vector3, FireballView>
  {
  }
}