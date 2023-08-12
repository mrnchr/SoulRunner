using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class KelliJumpState : PlayerJumpState
  {
    public override void ProcessInput(InputValues inputs)
    {
      base.ProcessInput(inputs);
      if (inputs.SideAbilityButton)
        Machine.ChangeState(PlayerStateType.SideAbility);
      else if (inputs.MainAbilityButton)
        Machine.ChangeState(PlayerStateType.MainAbility);
    }
  }
}