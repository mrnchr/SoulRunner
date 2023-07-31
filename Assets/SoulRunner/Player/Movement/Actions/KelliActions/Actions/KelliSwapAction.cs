using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class KelliSwapAction : PlayerSwapAction, IKelliAction
  {
    public KelliSwapAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public override void Swap()
    {
      if (!IsActive || _chars.SwapDelay.Current > 0) return;
      
      _view.KelliMesh.enabled = false;
      _view.ShonMesh.enabled = true;
      _chars.Hero.Current = HeroType.Shon;
      _variables.OnSwap?.Invoke(_chars.Hero);
      TimerManager.AddTimer(_chars.SwapDelay.Current = _chars.SwapDelay.Max);
    }
  }
}