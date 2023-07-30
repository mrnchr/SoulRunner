using SoulRunner.Configuration;
using SoulRunner.Infrastructure.Spine;
using Spine.Unity;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballAnimator : MonoBehaviour
  {
    public SkeletonAnimation Skeleton;
    public FireballAnim Assets;

    private SpineAnimator<FireballAnimType> _animator;
    private bool _isDead;


    public bool IsDead
    {
      get => _isDead;
      set => _animator.SetVariable(value, ref _isDead);
    }

    private void Awake()
    {
      _animator = new SpineAnimator<FireballAnimType>(Skeleton, Assets.Anims, start: FireballAnimType.Spawn);

      _animator
        .AddTransition(FireballAnimType.Spawn, FireballAnimType.Idle, () => true, true)
        .AddTransition(FireballAnimType.Idle, FireballAnimType.Death, () => IsDead);
    }

    private void Start()
    {
      _animator.StartAnimate();
    }
  }
}