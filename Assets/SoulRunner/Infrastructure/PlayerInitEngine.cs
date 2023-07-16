using Leopotam.EcsLite;
using SoulRunner.Player;
using SoulRunner.Utility.Ecs.Combine;
using UnityEngine;

namespace SoulRunner.Infrastructure
{
  public class PlayerInitEngine : IEcsEngine
  {
    public void Start(IEcsSystems systems)
    {
      systems
        .Add(new CreatePlayerSystem())
        .Add(new SpawnPlayerSystem());
    }
  }
}