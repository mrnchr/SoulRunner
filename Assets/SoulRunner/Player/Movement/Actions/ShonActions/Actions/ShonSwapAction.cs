using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class ShonSwapAction : PlayerSwapAction, IShonAction
  {
    public ShonSwapAction(PlayerView view) : base(view)
    {
    }

    public override void Swap()
    {
      if (_variables.ActiveHero != HeroType.Shon || _variables.SwapDelay > 0) return;
      
      _view.ShonMesh.enabled = false;
      _view.KelliMesh.enabled = true;
      _variables.ActiveHero = HeroType.Kelli;
      _variables.OnSwap?.Invoke(_variables.ActiveHero);
      TimerManager.AddTimer(_variables.SwapDelay = _view.PlayerCfg.SwapDelay);
    }
  }
}