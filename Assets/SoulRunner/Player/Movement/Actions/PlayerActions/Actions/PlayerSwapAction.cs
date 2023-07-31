using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerSwapAction : PlayerMovementAction, ISwapAction
  {
    public PlayerSwapAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Swap()
    {
    }
  }
}