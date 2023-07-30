using System;

namespace SoulRunner.Player
{
  public class KelliAnimSwitcher : PlayerAnimSwitcher
  {
    protected override void Activate()
    {
      base.Activate();
      
      _variables.OnDashStart += AnimateDash;
      _variables.OnFireStart += AnimateFire;
    }

    protected override void Deactivate()
    {
      base.Deactivate();
      
      _variables.OnDashStart -= AnimateDash;
      _variables.OnFireStart -= AnimateFire;
    }

    protected override void AnimateMove(float direction) => _player.KelliAnim.IsRun = direction != 0;
    protected override void AnimateJump(bool isJump) => _player.KelliAnim.IsJump = isJump;
    protected override void AnimateCrouch(bool isCrouch) => _player.KelliAnim.IsCrouch = isCrouch;
    protected override void AnimateClimb(bool isClimb) => _player.KelliAnim.IsClimb = isClimb;
    protected override void AnimateFall(bool isFall) => _player.KelliAnim.IsFall = isFall;
    protected override void AnimateSwap(HeroType _) => _player.KelliAnim.SwapTrigger = true;

    private void AnimateFire(HandType fireHand)
    {
      switch (fireHand)
      {
        case HandType.Left:
          _player.KelliAnim.LeftFireTrigger = true;
          break;
        case HandType.Right:
          _player.KelliAnim.RightFireTrigger = true;
          break;
        case HandType.None:
        default:
          throw new ArgumentOutOfRangeException(nameof(fireHand), fireHand, null);
      }
    }

    private void AnimateDash() => _player.KelliAnim.DashTrigger = true;
  }
}