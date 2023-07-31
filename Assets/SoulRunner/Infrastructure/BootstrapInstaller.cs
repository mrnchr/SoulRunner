using SoulRunner.Configuration;
using UnityEngine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private ConfigList _configList;
    [SerializeField] private SpecList _specList;

    public override void InstallBindings()
    {
      BindTimerManager();
      BindConfigService();
      BindSpecService();
    }

    private void BindSpecService()
    {
      Container
        .Bind<ISpecificationService>()
        .To<SpecificationService>()
        .FromInstance(new SpecificationService(_specList.Specs));
    }

    private void BindConfigService()
    {
      Container
        .Bind<IConfigService>()
        .To<ConfigService>()
        .FromInstance(new ConfigService(_configList.Configs));
    }

    private void BindTimerManager()
    {
      Container
        .Bind<IFixedTickable>()
        .To<TimerManager>()
        .AsSingle();
    }
  }
}