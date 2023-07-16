using System.Collections.Generic;
using System.Linq;

namespace SoulRunner.Utility.Ecs.Combine
{
  public abstract class EcsCombineInjector
  {
    protected readonly List<object> _injects;

    protected EcsCombineInjector(params object[] injects)
    {
      _injects = injects.ToList();
    }

    public List<object> injects => _injects;
  }
}