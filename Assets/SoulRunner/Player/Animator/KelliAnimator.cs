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
    private bool _needCheck;

    [SerializeField] private bool _dashTrigger;
    public bool DashTrigger
    {
      private get => _dashTrigger;
      set => SetVariable(value, ref _dashTrigger);
    }

    [SerializeField] private bool _rightFireTrigger;
    public bool RightFireTrigger
    {
      private get => _rightFireTrigger;
      set => SetVariable(value, ref _rightFireTrigger);
    }

    [SerializeField] private bool _leftFireTrigger;
    public bool LeftFireTrigger
    {
      private get => _leftFireTrigger;
      set => SetVariable(value, ref _leftFireTrigger);
    }
    
    [SerializeField] private bool _isRun;
    public bool IsRun
    {
      get => _isRun;
      set => SetVariable(value, ref _isRun);
    }

    [SerializeField] private bool _isJump;
    public bool IsJump
    {
      get => _isJump;
      set => SetVariable(value, ref _isJump);
    }

    [SerializeField] private bool _isCrouch;
    public bool IsCrouch
    {
      get => _isCrouch;
      set => SetVariable(value, ref _isCrouch);
    }

    [SerializeField] private bool _isClimb;
    public bool IsClimb
    {
      get => _isClimb;
      set => SetVariable(value, ref _isClimb);
    }

    [SerializeField] private bool _isFall;
    public bool IsFall
    {
      get => _isFall;
      set => SetVariable(value, ref _isFall);
    }
    
    private void SetVariable<T>(T value, ref T variable)
    {
      _needCheck = !value.Equals(variable);
      variable = value;
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
        .From(KelliAnimType.Idle, KelliAnimType.JumpLand)
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
        .AddTransition(KelliAnimType.JumpStart, KelliAnimType.JumpIdle, () => IsFall)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.JumpLand, () => !IsJump && !IsCrouch)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.Crouch, () => !IsJump && IsCrouch)
        .AddTransition(KelliAnimType.Crouch, KelliAnimType.Idle, () => !IsCrouch)
        .AddTransition(KelliAnimType.Dash, KelliAnimType.Idle, () => !IsJump, true)
        .AddTransition(KelliAnimType.Dash, KelliAnimType.JumpIdle, () => IsJump, true)
        .AddTransition(KelliAnimType.Idle, KelliAnimType.Climb, () => IsClimb)
        .AddTransition(KelliAnimType.Run, KelliAnimType.Climb, () => IsClimb)
        .AddTransition(KelliAnimType.JumpIdle, KelliAnimType.Climb, () => IsClimb)
        .AddTransition(KelliAnimType.JumpLand, KelliAnimType.Climb, () => IsClimb)
        .AddTransition(KelliAnimType.Dash, KelliAnimType.Climb, () => IsClimb)
        .AddTransition(KelliAnimType.Climb, KelliAnimType.Idle, () => !IsClimb)
        .AddTransition(KelliAnimType.JumpStart, KelliAnimType.JumpIdle, () => true, true)
        .AddTransition(KelliAnimType.JumpLand, KelliAnimType.Idle, () => true, true)

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

    private void Update()
    {
      if (_needCheck)
      {
        _animator.CheckTransition();
        _needCheck = false;
      }

      ClearTriggers();
    }

    private void ClearTriggers()
    {
      _dashTrigger = false;
      _leftFireTrigger = false;
      _rightFireTrigger = false;
    }
  }
}