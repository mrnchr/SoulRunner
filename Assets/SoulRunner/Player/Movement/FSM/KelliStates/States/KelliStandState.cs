using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class KelliStandState : PlayerStandState
  {
    public override void ProcessInput(InputValues inputs)
    {
      base.ProcessInput(inputs);
      if (inputs.MainAbilityButton)
        Machine.ChangeState(PlayerStateType.MainAbility);
    }
  }
}