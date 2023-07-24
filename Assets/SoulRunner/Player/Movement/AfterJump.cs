using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public struct AfterJump : ITimerable
  {
    public float TimeLeft { get; set; }
  }
}