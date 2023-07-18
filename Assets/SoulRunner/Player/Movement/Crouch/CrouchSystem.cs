using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class CrouchSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<CrouchCommand, Crouchable, PlayerViewRef>, Exc<Crouching, InJump>> _crouches = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _crouches.Value)
      {
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        view.StayCollider.enabled = false;
        view.CrouchCollider.enabled = true;
        view.Rb.velocity = Vector2.zero;

        _world.Add<Crouching>(index);
      }
    }
  }
}