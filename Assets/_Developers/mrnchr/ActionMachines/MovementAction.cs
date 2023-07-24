namespace SoulRunner.Player.ActionMachines
{
  public abstract class MovementAction<TView>
  where TView : View
  {
    protected TView _view;

    protected MovementAction(TView view)
    {
      _view = view;
    }
  }
}