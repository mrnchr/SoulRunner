using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class ShonFallState : PlayerFallState
  {
    public override void ProcessInput(InputValues inputs)
    {
      base.ProcessInput(inputs);
      if (inputs.JumpButton)
        Machine.ChangeState(PlayerStateType.Jump);
    }
  }
}