using SoulRunner.Utility.Ecs;

namespace SoulRunner.Player.Movement
{
  public struct FireTimer : ITimerable
  {
    public float TimeLeft { get; set; }
    public HandType Hand;
  }
}