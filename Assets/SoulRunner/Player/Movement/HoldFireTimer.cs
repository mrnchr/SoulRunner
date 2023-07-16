using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public struct HoldFireTimer : ITimerable
  {
    public float TimeLeft { get; set; }
  }
}