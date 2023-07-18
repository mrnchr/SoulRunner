using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public struct DelayDash : ITimerable
  {
    public float TimeLeft { get; set; }
  }
}