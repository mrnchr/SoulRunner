using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Player
{
  public class AnimSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<StartMove, PlayerViewRef>> _moving = default;
    private readonly EcsFilterInject<Inc<StartJump, PlayerViewRef>> _jumping = default;
    private readonly EcsFilterInject<Inc<InJump, OnGround, PlayerViewRef>> _landing = default;
    private readonly EcsFilterInject<Inc<Crouching, PlayerViewRef>> _crouching = default;
    private readonly EcsFilterInject<Inc<StartStand, PlayerViewRef>> _standing = default;
    private readonly EcsFilterInject<Inc<StartFire, Kelli, PlayerViewRef>> _firing = default;
    private readonly EcsFilterInject<Inc<StartDash, Kelli, PlayerViewRef>> _dashing = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      AnimateMoving();
      AnimateJumping();
      AnimateLanding();
      AnimateCrouching();
      AnimateStanding();
      AnimateFiring();
      AnimateDash();
    }

    private void AnimateDash()
    {
      foreach (int index in _dashing.Value)
        _world.Get<PlayerViewRef>(index).Value.CurrentAnimator.DashTrigger = true;
    }

    private void AnimateFiring()
    {
      foreach (int index in _firing.Value)
      {
        HandType hand = _world.Get<StartFire>(index).Hand;
        PlayerView view = _world.Get<PlayerViewRef>(index).Value;
        switch (hand)
        {
          case HandType.Left:
            view.CurrentAnimator.LeftFireTrigger = true;
            break;
          case HandType.Right:
            view.CurrentAnimator.RightFireTrigger = true;
            break;
          case HandType.None:
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private void AnimateStanding()
    {
      foreach (int index in _standing.Value)
        _world.Get<PlayerViewRef>(index).Value.CurrentAnimator.IsCrouch = false;
    }

    private void AnimateCrouching()
    {
      foreach (int index in _crouching.Value)
        _world.Get<PlayerViewRef>(index).Value.CurrentAnimator.IsCrouch = true;
    }

    private void AnimateLanding()
    {
      foreach (int index in _landing.Value)
      {
        _world.Get<PlayerViewRef>(index).Value.CurrentAnimator.IsJump = false;
        _world.Del<InJump>(index);
      }
    }

    private void AnimateJumping()
    {
      foreach (int index in _jumping.Value)
      {
        _world.Get<PlayerViewRef>(index).Value.CurrentAnimator.IsJump = true;
        _world.Update<InJump>(index);
      }
    }

    private void AnimateMoving()
    {
      foreach (int index in _moving.Value)
      {
        float direction = _world.Get<StartMove>(index).Direction;
        PlayerView player = _world.Get<PlayerViewRef>(index).Value;

        player.CurrentAnimator.IsRun = direction != 0;
        player.transform.localScale = SetViewDirection(player.transform.localScale, direction);
      }
    }

    private Vector3 SetViewDirection(Vector3 scale, float moveDirection)
    {
      if (moveDirection * scale.x < 0)
        scale.x *= -1;
      return scale;
    }
  }
}