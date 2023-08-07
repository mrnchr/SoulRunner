namespace SoulRunner.Infrastructure
{
  public interface IConfigurationService
  {
    public TConfig GetConfig<TConfig>()
      where TConfig : IConfig;
  }
}