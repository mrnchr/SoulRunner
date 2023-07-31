namespace SoulRunner.Infrastructure.Actions
{
  public abstract class MovementAction<TView>
  where TView : View
  {
    protected ActionMachine<TView> _machine;
    protected TView _view;

    protected MovementAction(ActionMachine<TView> machine)
    {
      _machine = machine;
      _view = _machine.View;
    }
  }
}