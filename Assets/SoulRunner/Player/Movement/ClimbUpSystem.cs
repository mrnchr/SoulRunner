using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class ClimbUpSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<UpCommand, Climbing, PlayerViewRef>, Exc<EndFall>> _up = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _up.Value)
      {
        ref Jumpable jumpable = ref _world.Get<Jumpable>(index);
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        view.Rb.gravityScale = 1;
        view.Rb.AddForce(Vector2.up * jumpable.JumpForce, ForceMode2D.Impulse);
        Debug.Log("Climb Up");
        _world.DelSoftly<OnGround>(index);
        _world.Del<Climbing>(index);
        _world.Add<EndClimb>(index);
        _world.Add<StartJump>(index);
        _world.Add<AfterJump>(index)
          .TimeLeft = 0.1f;
        _world.Add<Jumping>(index);
      }
    }
    
    private Vector2 ResetYToZero(Vector2 direction)
    {
      direction.y = 0;
      return direction;
    }
  }
}