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
      if (!IsActive || _chars.SwapDelay.Current > 0 || _variables.IsAttacking || _variables.IsAttackInJump) return;
      
      _view.KelliMesh.enabled = false;
      _view.ShonMesh.enabled = true;
      _chars.Hero.Current = ObjectType.Shon;
      _variables.OnSwap?.Invoke(_chars.Hero);
      _chars.SwapDelay.ToDefault();
      TimerManager.AddTimer(_chars.SwapDelay.Current);
    }
  }
}