using SoulRunner.Control;
using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class PlayerSwapState : PlayerState
  {
    public override bool CanStart() => View.Chars.SwapDelay.Current <= 0;

    public override void Start()
    {
    }

    public override void End()
    {
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (inputs.SwapHeroButton && View.Chars.SwapDelay.Current <= 0 &&
        Machine.CurrentState is not PlayerStateType.SuperAttack and not PlayerStateType.SideAbility)
        Swap();
    }

    public virtual void Swap()
    {
      Machine.ReplaceState(PlayerStateType.Empty);

      Variables.OnSwap?.Invoke(View.Chars.Hero.Current);
      View.Chars.SwapDelay.ToDefault();
      TimerManager.AddTimer(View.Chars.SwapDelay.Current);
    }
  }
}