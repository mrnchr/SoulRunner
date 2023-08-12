using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class PlayerEmptyState : PlayerState
  {
    public override bool CanStart() => true;

    public override void Start()
    {
    }

    public override void End()
    {
    }

    public override void ProcessInput(InputValues inputs)
    {
    }
  }
}