using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public struct Dashing : ITimerable
  {
    public float TimeLeft { get; set; }
  }
}