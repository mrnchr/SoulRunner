using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public struct DelayFire : ITimerable
  {
    public float TimeLeft { get; set; }
  }
}