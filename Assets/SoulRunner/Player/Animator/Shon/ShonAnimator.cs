using SoulRunner.Configuration;
using SoulRunner.Infrastructure.Spine;

namespace SoulRunner.Player
{
  public class ShonAnimator : PlayerAnimator<ShonAnimType>
  {
    public ShonAnim Assets;

    private void Awake()
    {
      _animator = new SpineAnimator<ShonAnimType>(Skeleton, Assets.Anims, start: ShonAnimType.Idle);

      _animator
        // layer #0  
        .CreateTransition()
        .From(ShonAnimType.Idle, ShonAnimType.JumpLand, ShonAnimType.JumpDown)
        .To(ShonAnimType.Run)
        .End(() => IsRun)
        .CreateTransition()
        .From(ShonAnimType.Idle, ShonAnimType.Run)
        .To(ShonAnimType.JumpStart)
        .End(() => IsJump)
        .CreateTransition()
        .From(ShonAnimType.Idle, ShonAnimType.Run, ShonAnimType.JumpLand, ShonAnimType.JumpDown)
        .To(ShonAnimType.Crouch)
        .End(() => IsCrouch)
        .AddTransition(ShonAnimType.Run, ShonAnimType.Idle, () => !IsRun)
        .AddTransition(ShonAnimType.Crouch, ShonAnimType.Idle, () => !IsCrouch)
        .AddTransition(ShonAnimType.JumpIdle, ShonAnimType.JumpDown, () => !IsJump)
        .AddTransition(ShonAnimType.JumpLand, ShonAnimType.JumpIdle, () => IsJump)
        .AddTransition(ShonAnimType.JumpDown, ShonAnimType.JumpIdle, () => IsJump)
        .AddTransition(ShonAnimType.JumpDown, ShonAnimType.JumpLand, () => true, true)
        .AddTransition(ShonAnimType.JumpLand, ShonAnimType.Idle, () => true, true)
        .AddTransition(ShonAnimType.JumpStart, ShonAnimType.JumpIdle, () => true, true);
    }
  }
}