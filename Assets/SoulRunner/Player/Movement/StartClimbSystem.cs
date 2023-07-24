using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class StartClimbSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<OnLedge>, Exc<Climbing, Crouching, Jumping, Falling>> _ledge = default;
    private readonly EcsFilterInject<Inc<OnLedge, Jumping>, Exc<AfterJump>> _jumping = default;
    private readonly EcsFilterInject<Inc<OnLedge, UpCommand, Falling>> _falling = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _ledge.Value)
      {
        Debug.Log("Just Climb");
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        Climb(index, view);
      }

      foreach (int index in _jumping.Value)
      {
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        if (view.Rb.velocity.y > 0) continue;
        
        Debug.Log("Jump Climb");
        Climb(index, view);
        DelJump(index);
      }

      foreach (int index in _falling.Value)
      {
        Debug.Log("Fall Climb");
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        Climb(index, view);
        DelFall(index);
      }
    }

    private void DelFall(int index)
    {
      if (_world.Has<Falling>(index))
      {
        _world.Del<Falling>(index);
        _world.Add<EndFall>(index);
      }
    }

    private void DelJump(int index)
    {
      if (_world.Has<Jumping>(index))
      {
        _world.Del<Jumping>(index);
        _world.Add<EndJump>(index);
      }
    }

    private void Climb(int entity, PlayerView view)
    {
      _world.Add<StartClimb>(entity);
      _world.Add<Climbing>(entity);
      _world.DelSoftly<Dashing>(entity);

      view.Rb.gravityScale = 0;
      view.Rb.velocity = Vector2.zero;
    }
  }
}