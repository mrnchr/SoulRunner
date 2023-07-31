using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class ShonLandAction : PlayerLandAction, IShonAction
  {
    public ShonLandAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public override void Land()
    {
      if (!IsActive) return;

      base.Land();
      _variables.CanAirJump = true;
    }
  }
}