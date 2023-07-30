namespace SoulRunner.Player
{
  public class KelliJumpAction : PlayerJumpAction, IKelliAction
  {
    public KelliJumpAction(PlayerView view) : base(view)
    {
    }

    public override void Jump()
    {
      if (!IsActive) return;
      base.Jump();
    }
  }
}