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
    public Action<ObjectType> OnSwap;

    public Action OnDashStart;
    public Action OnDashEnd;
    public Action<HandType> OnFireStart;
    public Action<HandType> OnFire;
    public Action OnAttackInJumpStart;
    public Action OnAttackInJumpEnd;
    public Action OnAttackStart;
    public Action OnAttackEnd;

    public Action OnAirJump;

    public bool IsOnGround;
    public bool IsOnLedge;
    public float LedgePosX;
    public bool IsJumping;
    public bool IsCrouching;
    public bool IsFiring;
    public bool IsClimbing;
    public bool IsFalling;
    
    public bool IsDashing;
    public bool IsAttackInJump;
    public bool IsAttacking;
    public HandType NextHand;

    public bool CanAirJump;
  }
}