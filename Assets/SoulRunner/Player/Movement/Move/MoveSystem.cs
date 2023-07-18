using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player
{
  public class MoveSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<MoveCommand, PlayerViewRef>, Exc<Crouching, Dashing>> _moves = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _moves.Value)
      {
        Rigidbody2D rb = _world.Get<PlayerViewRef>(index).Value.Rb;
        float direction = _world.Get<MoveCommand>(index).Direction;
        float speed = _world.Get<Movable>(index).Speed;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        _world.Add<StartMove>(index).Direction = direction;
      }
    }
  }
}