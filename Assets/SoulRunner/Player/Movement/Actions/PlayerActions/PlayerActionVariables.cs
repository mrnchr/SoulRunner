using System;
using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  [Serializable]
  public class PlayerActionVariables
  {
    public Action<float> OnMove;
    public Action OnJumpStart;
    public Action OnJumpEnd;
    public Action OnFallStart;
    public Action OnFallEnd;
    public Action OnCrouchStart;
    public Action OnCrouchEnd;
    public Action OnClimbStart;
    public Action OnClimbEnd;
    public Action<HeroType> OnSwap;

    public Action OnDashStart;
    public Action OnDashEnd;
    public Action<HandType> OnFireStart;
    public Action<HandType> OnFire;

    public HeroType ActiveHero;
    public bool IsOnGround;
    public bool IsOnLedge;
    public float LedgePosX;
    public bool IsJumping;
    public bool IsCrouching;
    public bool IsDashing;
    public bool IsFiring;
    public bool IsClimbing;
    public bool IsFalling;
    public HandType NextHand;
    public Timer BeforeFire = 0;
    public Timer FireDelay = 0;
    public Timer DashDelay = 0;
    public Timer SwapDelay = 0;
  }
}