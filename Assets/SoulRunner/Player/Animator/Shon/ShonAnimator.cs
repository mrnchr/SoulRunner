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
        .From(ShonAnimType.Idle)
        .To(ShonAnimType.Run)
        .End(() => IsRun)
        .CreateTransition()
        .From(ShonAnimType.Idle, ShonAnimType.Run)
        .To(ShonAnimType.Crouch)
        .End(() => IsCrouch)
        .AddTransition(ShonAnimType.Run, ShonAnimType.Idle, () => !IsRun)
        .AddTransition(ShonAnimType.Crouch, ShonAnimType.Idle, () => !IsCrouch);
    }
  }
}