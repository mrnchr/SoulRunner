using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class EndDashSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<Dashing, PlayerViewRef, Kelli>> _dashing = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _dashing.Value)
      {
        if(_world.Get<Dashing>(index).TimeLeft > 0) continue;

        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        view.gameObject.layer = (int)Mathf.Log(view.DefaultLayer, 2);
        view.Rb.gravityScale = 1;
        
        _world.Del<Dashing>(index);
      }
    }
  }
}