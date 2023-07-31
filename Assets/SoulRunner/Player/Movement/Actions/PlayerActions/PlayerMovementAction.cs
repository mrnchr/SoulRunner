using SoulRunner.Configuration;
using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class PlayerMovementAction : MovementAction<PlayerView>, ICycleAction
  {
    public bool IsActive { get; set; }
    protected readonly PlayerActionVariables _variables;
    protected readonly PlayerChars _chars;
    protected readonly PlayerSpec _spec;

    public PlayerMovementAction(ActionMachine<PlayerView> machine) : base(machine)
    {
      _chars = _view.Chars;
      _spec = _view.Spec;
      _variables = _view.ActionVariables;
    }

    public virtual void Activate()
    {
      IsActive = true;
    }

    public virtual void Deactivate()
    {
      IsActive = false;
    }
  }
}