using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerAnimSwitcher : MonoBehaviour
  {
    [SerializeField] protected PlayerView _player;
    protected PlayerStateVariables _variables;

    private void Awake()
    {
      _variables = _player.StateVariables;
    }

    private void OnEnable()
    {
      Activate();
    }

    private void OnDisable()
    {
      Deactivate();
    }

    protected virtual void Activate()
    {
      _variables.OnMove += AnimateMove;
      _variables.OnJumpStart += () => AnimateJump(true);
      _variables.OnJumpEnd += () => AnimateJump(false);
      _variables.OnFallStart += () => AnimateFall(true);
      _variables.OnFallEnd += () => AnimateFall(false);
      _variables.OnCrouchStart += () => AnimateCrouch(true);
      _variables.OnCrouchEnd += () => AnimateCrouch(false);
      _variables.OnClimbStart += () => AnimateClimb(true);
      _variables.OnClimbEnd += () => AnimateClimb(false);
      _variables.OnSwap += AnimateSwap;
    }

    protected virtual void Deactivate()
    {
      _variables.OnMove -= AnimateMove;
      _variables.OnJumpStart -= () => AnimateJump(true);
      _variables.OnJumpEnd -= () => AnimateJump(false);
      _variables.OnFallStart -= () => AnimateFall(true);
      _variables.OnFallEnd -= () => AnimateFall(false);
      _variables.OnCrouchStart -= () => AnimateCrouch(true);
      _variables.OnCrouchEnd -= () => AnimateCrouch(false);
      _variables.OnClimbStart -= () => AnimateClimb(true);
      _variables.OnClimbEnd -= () => AnimateClimb(false);
      _variables.OnSwap -= AnimateSwap;
    }
    
    protected virtual void AnimateMove(float direction) 
    {
    }
    protected virtual void AnimateJump(bool isJump) 
    {
    }
    protected virtual void AnimateCrouch(bool isCrouch) 
    {
    }
    protected virtual void AnimateClimb(bool isClimb) 
    {
    }
    protected virtual void AnimateFall(bool isFall) 
    {
    }
    protected virtual void AnimateSwap(ObjectType _) 
    {
    }
  }
}