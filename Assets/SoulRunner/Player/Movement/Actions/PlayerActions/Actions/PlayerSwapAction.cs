using SoulRunner.Configuration;

namespace SoulRunner.Player
{
  public class PlayerSwapAction : PlayerCycleAction, ISwapAction
  {
    protected PlayerConfig _playerCfg;

    public PlayerSwapAction(PlayerView view) : base(view)
    {
      _playerCfg = view.PlayerCfg;
    }

    public virtual void Swap()
    {
    }
  }
}