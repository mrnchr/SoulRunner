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
      BindDamageService();
    }

    private void BindDamageService()
    {
      Container
        .Bind<IDamageService>()
        .To<DamageService>()
        .AsSingle();
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
        .Bind<IConfigurationService>()
        .To<ConfigurationService>()
        .FromInstance(new ConfigurationService(_configList.Configs));
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