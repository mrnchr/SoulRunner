using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class ShonSwapState : PlayerSwapState
  {
    public override void Swap()
    {
      Machine.IsActive = false;
      View.ShonMesh.enabled = false;

      View.Chars.Hero.Current = ObjectType.Kelli;
      View.KelliMachine.ReplaceState(Machine.CurrentState);
      View.KelliMesh.enabled = true;
      View.KelliMachine.IsActive = true;

      base.Swap();
    }
  }
}