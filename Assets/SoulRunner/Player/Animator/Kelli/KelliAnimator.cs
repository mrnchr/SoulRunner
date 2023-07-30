using System.Linq;
using SoulRunner.Configuration;
using SoulRunner.Infrastructure.Spine;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliAnimator : PlayerAnimator<KelliAnimType>
  {
    public KelliAnim Assets;

    public bool DashTrigger
    {
      private get => _dashTrigger;
      set => SetVariable(value, ref _dashTrigger);
    }


    public bool RightFireTrigger
    {
      private get => _rightFireTrigger;
      set => SetVariable(value, ref _rightFireTrigger);
    }


    public bool LeftFireTrigger
    {
      private get => _leftFireTrigger;
      set => SetVariable(value, ref _leftFireTrigger);
    }

    [SerializeField] private bool _rightFireTrigger;
    [SerializeField] private bool _leftFireTrigger;
    [SerializeField] private bool _dashTrigger;

    private void Awake()
    {
      _animator = new SpineAnimator<KelliAnimType>(Skeleton, Assets.Anims, 2);

      _animator
        .AddAnimationsToLayer(0, KelliAnimType.Idle,
          Assets.Anims.Where(x =>
            x.Name != KelliAnimType.WallFire &&
            x.Name != KelliAnimType.FireLeftHand &&
            x.Name != KelliAnimType.FireRightHand).ToArray())
        .AddAnimationsToLayer(1, KelliAnimType.Empty,
          KelliAnimType.Empty,
          KelliAnimType.WallFire,
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
        .AddTransition(KelliAnimType.JumpLand, KelliAnimType.JumpIdle, () => IsJump)
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
        .AddTransition(KelliAnimType.Empty, KelliAnimType.FireLeftHand, () => LeftFireTrigger && !IsClimb)
        .AddTransition(KelliAnimType.Empty, KelliAnimType.FireRightHand, () => RightFireTrigger && !IsClimb)
        .AddTransition(KelliAnimType.Empty, KelliAnimType.WallFire, () => IsClimb && (LeftFireTrigger || RightFireTrigger))
        .CreateTransition()
        .From(KelliAnimType.FireLeftHand, KelliAnimType.FireRightHand, KelliAnimType.WallFire)
        .To(KelliAnimType.Empty)
        .End(() => true, true);
    }

    protected override void ClearTriggers()
    {
      base.ClearTriggers();
      _dashTrigger = false;
      _leftFireTrigger = false;
      _rightFireTrigger = false;
    }
  }
}