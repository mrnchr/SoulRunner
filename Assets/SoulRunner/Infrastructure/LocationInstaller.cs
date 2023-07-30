using SoulRunner.Configuration;
using SoulRunner.Configuration.Source;
using SoulRunner.Control;
using SoulRunner.Fireball;
using SoulRunner.Player;
using UnityEngine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class LocationInstaller : MonoInstaller
  {
    [SerializeField] private InputReader _input;
    [SerializeField] private HeroSo _heroSo;
    [SerializeField] private PrefabData _prefabs;
    [SerializeField] private LevelManagement.Level _level;
    [SerializeField] private FireballSo _fireballSo;

    public override void InstallBindings()
    {
      BindHero();
      BindFireball();
      
      BindLevel();
      BindInstallerInterfaces();
      
      BindPrefabData();
      BindPrefabService();
      
      BindPlayerFactory();
      BindFireballFactory();
      
      
      BindInputService();
    }

    private void BindFireball()
    {
      Container.BindInstance(_fireballSo.FireballCfg);
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

    private void BindHero() =>
      Container
        .BindInstance(_heroSo.PlayerCfg)
        .AsCached();

    private void BindInputService() =>
      Container
        .BindInstance(_input)
        .AsSingle();
  }
}