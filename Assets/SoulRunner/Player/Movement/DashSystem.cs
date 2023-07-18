using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class DashSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<DashCommand, PlayerViewRef, Kelli>, Exc<DelayDash, Crouching>> _ability = default;
    private readonly EcsCustomInject<HeroConfig> _heroCfg = default; 
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
        
        view.gameObject.layer = (int)Mathf.Log(_heroCfg.Value.DashLayer.value, 2);
        view.Rb.gravityScale = 0;
        view.Rb.velocity = Vector2.right * view.transform.localScale.x * _heroCfg.Value.DashSpeed;

        _world.Add<StartDash>(index);
        _world.Add<Dashing>(index)
          .TimeLeft = _heroCfg.Value.DashDuration;
        _world.Add<DelayDash>(index)
          .TimeLeft = _heroCfg.Value.DashDelay;
      }
    }
  }
}