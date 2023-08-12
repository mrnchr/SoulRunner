using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  public class KelliSwapState : PlayerSwapState
  {
    public override void Swap()
    {
      Machine.IsActive = false;
      View.KelliMesh.enabled = false;

      View.Chars.Hero.Current = ObjectType.Shon;
      View.ShonMachine.ReplaceState(Machine.CurrentState);
      View.ShonMesh.enabled = true;
      View.ShonMachine.IsActive = true;

      base.Swap();
    }
  }
}