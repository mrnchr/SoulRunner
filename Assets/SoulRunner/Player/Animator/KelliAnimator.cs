using System;
using System.Linq;
using SoulRunner.Configuration.Anim;
using SoulRunner.Utility.Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

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

    public bool RightFireTrigger
    {
      get => _rightFireTrigger;
      set
      {
        _animator.SetVariable(value, ref _rightFireTrigger);
        _rightFireTrigger = false;
      }
    }
    
    public bool LeftFireTrigger
    {
      get => _leftFireTrigger;
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
        .AddTransition(KelliAnimType.Crouch, KelliAnimType.Run, () => !IsCrouch && IsRun)
        
        // layer #1
        .AddTransition(KelliAnimType.Empty, KelliAnimType.FireLeftHand, () => LeftFireTrigger)
        .AddTransition(KelliAnimType.Empty, KelliAnimType.FireRightHand, () => RightFireTrigger)
        .AddTransition(KelliAnimType.FireLeftHand, KelliAnimType.Empty, () => true, true)
        .AddTransition(KelliAnimType.FireRightHand, KelliAnimType.Empty, () => true, true);
    }

    private void Start()
    {
      _animator.StartAnimate();
    }
  }
}