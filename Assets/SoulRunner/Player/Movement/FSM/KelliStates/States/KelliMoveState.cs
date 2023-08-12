using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class KelliMoveState : PlayerMoveState
  {
    public override void ProcessInput(InputValues inputs)
    {
      if (Machine.CurrentState is PlayerStateType.SuperAttack) return;
      base.ProcessInput(inputs);
    }
  }
}