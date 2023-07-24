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
    private readonly EcsFilterInject<Inc<PlayerViewRef>> _player = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _player.Value)
      {
        PlayerView player = _player.Pools.Inc1.Get(index).Value;
        KelliAnimator anim = player.CurrentAnimator;
        
        AnimateMove(index, anim, player);
        AnimateJump(index, anim);
        AnimateFall(index, anim);
        AnimateCrouch(index, anim);

        if (_world.Has<Kelli>(index))
        {
          AnimateFire(index, anim);
          AnimateDash(index, anim);
        }

        AnimateClimb(index, anim);
      }
    }

    private void AnimateFall(int index, KelliAnimator anim)
    {
      if (_world.Has<StartFall>(index))
      {
        anim.IsFall = true;
        anim.IsJump = true;
      }

      if (_world.Has<EndFall>(index))
      {
        anim.IsFall = false;
        anim.IsJump = false;
      }
    }

    private void AnimateClimb(int index, KelliAnimator anim)
    {
      if (_world.Has<StartClimb>(index))
      {
        anim.IsClimb = true;
        GlueViewToLedge(index);
      }

      if (_world.Has<EndClimb>(index))
        anim.IsClimb = false;
    }

    private void AnimateDash(int index, KelliAnimator anim)
    {
      if (_world.Has<StartDash>(index))
        anim.DashTrigger = true;
    }

    private void AnimateFire(int index, KelliAnimator anim)
    {
      if (_world.Has<StartFire>(index))
      {
        HandType hand = _world.Get<StartFire>(index).Hand;
        switch (hand)
        {
          case HandType.Left:
            anim.LeftFireTrigger = true;
            break;
          case HandType.Right:
            anim.RightFireTrigger = true;
            break;
          case HandType.None:
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private void AnimateCrouch(int index, KelliAnimator anim)
    {
      if (_world.Has<Crouching>(index))
        anim.IsCrouch = true;
      if (_world.Has<StartStand>(index))
        anim.IsCrouch = false;
    }

    private void AnimateJump(int index, KelliAnimator anim)
    {
      if (_world.Has<StartJump>(index))
        anim.IsJump = true;
      if (_world.Has<EndJump>(index))
        anim.IsJump = false;
    }

    private void AnimateMove(int index, KelliAnimator anim, PlayerView player)
    {
      if (_world.Has<StartMove>(index))
      {
        float direction = _world.Get<StartMove>(index).Direction;

        anim.IsRun = direction != 0;
        player.transform.localScale = SetViewDirection(player.transform.localScale, direction);
      }
    }

    private void GlueViewToLedge(int entity)
    {
      float posX = _world.Get<OnLedge>(entity).PosX;
      PlayerView view = _world.Get<PlayerViewRef>(entity).Value;
      Vector3 pos = view.transform.position;
      pos.x += posX - view.LedgeChecker.transform.position.x;
      view.transform.position = pos;
    }

    private Vector3 SetViewDirection(Vector3 scale, float moveDirection)
    {
      if (moveDirection * scale.x < 0)
        scale.x *= -1;
      return scale;
    }
  }
}