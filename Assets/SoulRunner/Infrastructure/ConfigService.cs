using System.Collections.Generic;
using System.Linq;
using SoulRunner.Configuration;

namespace SoulRunner.Infrastructure
{
  public class ConfigService : IConfigService
  {
    private List<Config> _configs;

    public ConfigService(List<Config> configs)
    {
      _configs = configs;
    }
    
    public TConfig GetConfig<TConfig>()
    where TConfig : IConfig =>
      _configs.OfType<TConfig>().First();
  }

  public interface IConfigService
  {
    public TConfig GetConfig<TConfig>()
      where TConfig : IConfig;
  }
}