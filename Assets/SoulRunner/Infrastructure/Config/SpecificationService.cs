using System.Collections.Generic;
using System.Linq;

namespace SoulRunner.Infrastructure
{
  public class SpecificationService : ISpecificationService
  {
    private List<Specification> _specs;

    public SpecificationService(List<Specification> specs)
    {
      _specs = specs;
    }

    public TSpec GetSpec<TSpec>()
    where TSpec : ISpec =>
      _specs.OfType<TSpec>().Single();
  }
}