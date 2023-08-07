using System.Collections.Generic;
using System.Linq;
using SoulRunner.Configuration;

namespace SoulRunner.Infrastructure
{
  public class ConfigurationService : IConfigurationService
  {
    private List<Config> _configs;

    public ConfigurationService(List<Config> configs)
    {
      _configs = configs;
    }
    
    public TConfig GetConfig<TConfig>()
    where TConfig : IConfig =>
      _configs.OfType<TConfig>().First();
  }
}