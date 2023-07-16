using System;
using SoulRunner.Configuration.Anim;
using SoulRunner.Utility.Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  public class KelliAnimator : MonoBehaviour
  {
    [FormerlySerializedAs("_skeleton")] public SkeletonAnimation Skeleton;
    [FormerlySerializedAs("_assets")] public KelliAnim Assets;

    private SpineAnimator<KelliAnimType> _animator;
    private bool _isJump;
    private bool _isRun;
    private bool _isCrouch;

    public bool IsRun
    {
      get => _isRun;
      set => _animator.SetVariable(value, ref _isRun);
    }

    public bool IsJump
    {
      get => _isJump;
      set => _animator.SetVariable(value, ref _isJump);
    }

    public bool IsCrouch
    {
      get => _isCrouch;
      set => _animator.SetVariable(value, ref _isCrouch);
    }

    private void Awake()
    {
      _animator = new SpineAnimator<KelliAnimType>(Skeleton, Assets.Anims, KelliAnimType.Idle);

      _animator
        .AddTransition(KelliAnimType.Idle, KelliAnimType.Run, () => IsRun)
        .AddTransition(KelliAnimType.Run, KelliAnimType.Idle, () => !IsRun)
        .AddTransition(KelliAnimType.Run, KelliAnimType.JumpStart, () => IsJump)
        .AddTransition(KelliAnimType.Idle, KelliAnimType.JumpStart, () => IsJump)
        .AddTransition(KelliAnimType.JumpStart, KelliAnimType.JumpIdle, () => true, true)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.JumpLand, () => !IsJump && !IsCrouch)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.Crouch, () => !IsJump && IsCrouch)
        .AddTransition(KelliAnimType.JumpLand, KelliAnimType.Idle, () => !IsRun, true)
        .AddTransition(KelliAnimType.JumpLand, KelliAnimType.Run, () => IsRun)
        .AddTransition(KelliAnimType.Idle, KelliAnimType.Crouch, () => IsCrouch)
        .AddTransition(KelliAnimType.Run, KelliAnimType.Crouch, () => IsCrouch)
        .AddTransition(KelliAnimType.Crouch, KelliAnimType.Idle, () => !IsCrouch && !IsRun)
        .AddTransition(KelliAnimType.Crouch, KelliAnimType.Run, () => !IsCrouch && IsRun);
    }

    private void Start()
    {
      _animator.StartAnimate();
    }
  }
}