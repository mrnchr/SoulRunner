using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class PlayerStandState : PlayerState
  {
    public override bool CanStart()
    {
      return true;
    }

    public override void Start()
    {
    }

    public override void End()
    {
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (inputs.JumpButton)
        Machine.ChangeState(PlayerStateType.Jump);
      else if (inputs.CrouchButton)
        Machine.ChangeState(PlayerStateType.Crouch);
      else if (!Variables.IsOnGround && View.Rb.velocity.y < 0)
        Machine.ChangeState(PlayerStateType.Fall);
    }
  }
}