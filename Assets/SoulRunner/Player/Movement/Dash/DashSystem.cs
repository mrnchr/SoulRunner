using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class DashSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<DashCommand, PlayerViewRef, Kelli>, Exc<DelayDash, Crouching, Climbing>> _ability = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _ability.Value)
      {
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        ref Dashable dashable = ref _world.Get<Dashable>(index);
        
        view.gameObject.layer = (int)Mathf.Log(view.DashLayer, 2);
        view.Rb.gravityScale = 0;
        view.Rb.velocity = Vector2.right * view.transform.localScale.x * dashable.DashSpeed;

        _world.Add<StartDash>(index);
        _world.Add<Dashing>(index)
          .TimeLeft = dashable.DashDuration;
        _world.Add<DelayDash>(index)
          .TimeLeft = dashable.DashDelay;
      }
    }
  }
}