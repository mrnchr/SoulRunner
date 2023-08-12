using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class ShonJumpState : PlayerJumpState
  {
    public override bool CanStart() => Machine.CurrentState != PlayerStateType.Fall || Variables.CanAirJump;

    public override void Start()
    {
      if (Machine.OldState == PlayerStateType.Fall)
        AirJump();
      else
        JumpForcefully();
    }

    public override void End()
    {
      Variables.CanAirJump = Machine.CurrentState is PlayerStateType.Stand or PlayerStateType.Climb;
      base.End();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (inputs.JumpButton && Variables.CanAirJump)
      {
        AirJump();
        return;
      }

      base.ProcessInput(inputs);
    }

    private void AirJump()
    {
      JumpForcefully();
      Variables.OnAirJump?.Invoke();
      Variables.CanAirJump = false;
    }
  }
}