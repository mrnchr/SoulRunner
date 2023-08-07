using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class ShonSwapAction : PlayerSwapAction, IShonAction
  {
    public ShonSwapAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public override void Swap()
    {
      if (!IsActive || _chars.SwapDelay.Current > 0) return;
      
      _view.ShonMesh.enabled = false;
      _view.KelliMesh.enabled = true;
      _chars.Hero.Current = ObjectType.Kelli;
      _variables.OnSwap?.Invoke(_chars.Hero);
      _chars.SwapDelay.ToDefault();
      TimerManager.AddTimer(_chars.SwapDelay.Current);
    }
  }
}