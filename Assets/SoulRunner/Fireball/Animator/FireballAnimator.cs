using SoulRunner.Configuration;
using SoulRunner.Infrastructure.Spine;
using Spine.Unity;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballAnimator : MonoSpineAnimator<FireballAnimType>
  {
    public FireballAnim Assets;

    private bool _deathTrigger;
    public bool DeathTrigger
    {
      get => _deathTrigger;
      set => _animator.SetVariable(value, ref _deathTrigger);
    }

    private void Awake()
    {
      _animator = new SpineAnimator<FireballAnimType>(Skeleton, Assets.Anims, start: FireballAnimType.Spawn);

      _animator
        .AddTransition(FireballAnimType.Spawn, FireballAnimType.Idle, () => true, true)
        .AddTransition(FireballAnimType.Idle, FireballAnimType.Death, () => DeathTrigger);
    }

    protected override void ClearTriggers()
    {
      _deathTrigger = false;
    }
  }
}