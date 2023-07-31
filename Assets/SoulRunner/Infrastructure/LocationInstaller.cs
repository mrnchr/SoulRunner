using SoulRunner.Configuration;
using SoulRunner.Control;
using SoulRunner.Fireball;
using SoulRunner.LevelManagement;
using SoulRunner.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class LocationInstaller : MonoInstaller
  {
    [SerializeField] private PrefabData _prefabs;
    [SerializeField] private Level _level;

    public override void InstallBindings()
    {
      BindLevel();
      BindInstallerInterfaces();
      
      BindPrefabData();
      BindPrefabService();
      
      BindPlayerFactory();
      BindFireballFactory();
      
      BindInputReader();
    }

    private void BindFireballFactory()
    {
      Container
        .Bind<IFireballFactory>()
        .To<FireballFactory>()
        .AsSingle();
    }

    private void BindLevel()
    {
      Container
        .BindInstance(_level)
        .AsCached();
    }

    private void BindPlayerFactory()
    {
      Container
        .Bind<IPlayerFactory>()
        .To<PlayerFactory>()
        .AsSingle();
    }

    private void BindInstallerInterfaces()
    {
      Container
        .BindInterfacesTo<LocationInstaller>()
        .FromInstance(this)
        .AsSingle();
    }

    private void BindPrefabService() =>
      Container
        .Bind<PrefabService>()
        .AsSingle();

    private void BindPrefabData() =>
      Container
        .BindInstance(_prefabs)
        .AsSingle();

    private void BindInputReader() =>
      Container
        .Bind(typeof(InputReader), typeof(ITickable), typeof(IInitializable))
        .To<InputReader>()
        .AsCached();
  }
}