using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player
{
  public class JumpSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<UpCommand, Jumpable, PlayerViewRef>, Exc<Crouching, Climbing, Falling>> _jumps = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _jumps.Value)
      {
        ref Jumpable jumpable = ref _world.Get<Jumpable>(index);
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        if (!_world.Has<OnGround>(index)) continue;
        
        view.Rb.velocity = ResetYToZero(view.Rb.velocity);
        view.Rb.AddForce(Vector2.up * jumpable.JumpForce, ForceMode2D.Impulse);
          
        _world.Del<OnGround>(index);
        _world.Add<StartJump>(index);
        _world.AddSoftly<Jumping>(index);
      }
    }

    private Vector2 ResetYToZero(Vector2 direction)
    {
      direction.y = 0;
      return direction;
    }
  }
}