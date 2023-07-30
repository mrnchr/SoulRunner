using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class KelliSwapAction : PlayerSwapAction, IKelliAction
  {
    public KelliSwapAction(PlayerView view) : base(view)
    {
    }

    public override void Swap()
    {
      if (!IsActive || _variables.SwapDelay > 0) return;
      
      _view.KelliMesh.enabled = false;
      _view.ShonMesh.enabled = true;
      _variables.ActiveHero = HeroType.Shon;
      _variables.OnSwap?.Invoke(_variables.ActiveHero);
      TimerManager.AddTimer(_variables.SwapDelay = _playerCfg.SwapDelay);
    }
  }
}