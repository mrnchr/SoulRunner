using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class ShonAnimSwitcher : PlayerAnimSwitcher
  {
    protected override void AnimateMove(float direction) => _player.ShonAnim.IsRun = direction != 0;
    protected override void AnimateJump(bool isJump) => _player.ShonAnim.IsJump = isJump;
    protected override void AnimateCrouch(bool isCrouch) => _player.ShonAnim.IsCrouch = isCrouch;
    protected override void AnimateClimb(bool isClimb) => _player.ShonAnim.IsClimb = isClimb;
    protected override void AnimateFall(bool isFall) => _player.ShonAnim.IsFall = isFall;
    protected override void AnimateSwap(ObjectType _) => _player.ShonAnim.SwapTrigger = true;
  }
}