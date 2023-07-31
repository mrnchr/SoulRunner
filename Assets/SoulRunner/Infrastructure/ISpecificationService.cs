using SoulRunner.Configuration;

namespace SoulRunner.Infrastructure
{
  public interface ISpecificationService
  {
    public TSpec GetSpec<TSpec>()
      where TSpec : ISpec;
  }
}