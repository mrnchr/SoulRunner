using System;
using System.Linq;
using SoulRunner.Configuration.Anim;
using SoulRunner.Utility.Spine;
using Spine.Unity;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliAnimator : MonoBehaviour
  {
    public SkeletonAnimation Skeleton;
    public KelliAnim Assets;

    private SpineAnimator<KelliAnimType> _animator;
    private bool _isJump;
    private bool _isRun;
    private bool _isCrouch;
    private bool _rightFireTrigger;
    private bool _leftFireTrigger;
    private bool _dashTrigger;

    public bool DashTrigger
    {
      private get => _dashTrigger;
      set
      {
        _animator.SetVariable(value, ref _dashTrigger);
        _dashTrigger = false;
      }
    }

    public bool RightFireTrigger
    {
      private get => _rightFireTrigger;
      set
      {
        _animator.SetVariable(value, ref _rightFireTrigger);
        _rightFireTrigger = false;
      }
    }

    public bool LeftFireTrigger
    {
      private get => _leftFireTrigger;
      set
      {
        _animator.SetVariable(value, ref _leftFireTrigger);
        _leftFireTrigger = false;
      }
    }

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
      _animator = new SpineAnimator<KelliAnimType>(Skeleton, Assets.Anims, 2);

      _animator.AddAnimationsToLayer(0, KelliAnimType.Idle,
          Assets.Anims.Where(x =>
            x.Name != KelliAnimType.FireWall &&
            x.Name != KelliAnimType.FireLeftHand &&
            x.Name != KelliAnimType.FireRightHand).ToArray())
        .AddAnimationsToLayer(1,
          KelliAnimType.Empty,
          KelliAnimType.Empty,
          KelliAnimType.FireWall,
          KelliAnimType.FireLeftHand,
          KelliAnimType.FireRightHand);

      _animator
        // layer #0  
        .CreateTransition()
        .From(KelliAnimType.Idle)
        .To(KelliAnimType.Run)
        .End(() => IsRun)
        .CreateTransition()
        .From(KelliAnimType.Idle, KelliAnimType.Run)
        .To(KelliAnimType.JumpStart)
        .End(() => IsJump)
        .CreateTransition()
        .From(KelliAnimType.Idle, KelliAnimType.Run)
        .To(KelliAnimType.Crouch)
        .End(() => IsCrouch)
        .CreateTransition()
        .From(KelliAnimType.Idle, KelliAnimType.Run, KelliAnimType.JumpStart, KelliAnimType.JumpIdle,
          KelliAnimType.JumpLand)
        .To(KelliAnimType.Dash)
        .End(() => DashTrigger)
        .AddTransition(KelliAnimType.Run, KelliAnimType.Idle, () => !IsRun)
        .AddTransition(KelliAnimType.JumpStart, KelliAnimType.JumpIdle, () => true, true)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.JumpLand, () => !IsJump && !IsCrouch)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.Crouch, () => !IsJump && IsCrouch)
        .AddTransition(KelliAnimType.JumpLand, KelliAnimType.Idle, () => true, true)
        .AddTransition(KelliAnimType.Crouch, KelliAnimType.Idle, () => !IsCrouch)
        .AddTransition(KelliAnimType.Dash, KelliAnimType.Idle, () => !IsJump, true)
        .AddTransition(KelliAnimType.Dash, KelliAnimType.JumpIdle, () => IsJump, true)

        // layer #1
        .AddTransition(KelliAnimType.Empty, KelliAnimType.FireLeftHand, () => LeftFireTrigger)
        .AddTransition(KelliAnimType.Empty, KelliAnimType.FireRightHand, () => RightFireTrigger)
        .CreateTransition()
        .From(KelliAnimType.FireLeftHand, KelliAnimType.FireRightHand)
        .To(KelliAnimType.Empty)
        .End(() => true, true);
    }

    private void Start()
    {
      _animator.StartAnimate();
    }
  }
}