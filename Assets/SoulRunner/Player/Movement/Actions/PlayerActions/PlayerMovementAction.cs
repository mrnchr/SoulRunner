using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerMovementAction : MovementAction<PlayerView>
  {
    protected readonly PlayerActionVariables _variables;

    public PlayerMovementAction(PlayerView view) : base(view)
    {
      _variables = view.ActionVariables;
    }
  }
}