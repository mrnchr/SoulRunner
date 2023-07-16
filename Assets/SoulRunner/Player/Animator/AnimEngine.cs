using Leopotam.EcsLite;
using SoulRunner.Utility.Ecs.Combine;

namespace SoulRunner.Player
{
  public class AnimEngine : IEcsEngine
  {
    public void Start(IEcsSystems systems)
    {
      systems
        .Add(new AnimSystem());
    }
  }
}