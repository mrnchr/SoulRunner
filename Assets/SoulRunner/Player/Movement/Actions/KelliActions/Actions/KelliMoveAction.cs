using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class KelliMoveAction : PlayerMoveAction
  {
    public KelliMoveAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }


    public override void Move(float direction)
    {
      if (_variables.IsAttacking) return;
      base.Move(direction);
    }
  }
}