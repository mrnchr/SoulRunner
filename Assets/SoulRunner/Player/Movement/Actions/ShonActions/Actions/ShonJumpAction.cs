namespace SoulRunner.Player
{
  public class ShonJumpAction : PlayerJumpAction, IShonAction
  {
    public ShonJumpAction(PlayerView view) : base(view)
    {
    }

    public override void Jump()
    {
      if (!IsActive) return;
      base.Jump();
    }
  }
}