using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerCrouchAction : PlayerMovementAction, ICrouchAction, IStandAction, IUpdateAction
  {
    protected bool _isCrouchCommand;

    public PlayerCrouchAction(PlayerView view) : base(view)
    {
    }

    public virtual void Crouch()
    {
      _isCrouchCommand = true;
      if (_variables.IsCrouching || _variables.IsJumping || _variables.IsClimbing || _variables.IsFalling) return;

      _view.StayCollider.enabled = false;
      _view.CrouchCollider.enabled = true;
      _view.Rb.velocity = Vector2.zero;

      _variables.IsCrouching = true;
      _variables.OnCrouchStart?.Invoke();
    }

    public virtual void Stand()
    {
      _view.CrouchCollider.enabled = false;
      _view.StayCollider.enabled = true;
      _variables.IsCrouching = false;
      _variables.OnCrouchEnd?.Invoke();
    }

    public virtual void Update()
    {
      if (_variables.IsCrouching)
      {
        if (!_isCrouchCommand)
          Stand();

        _isCrouchCommand = false;
      }
    }
  }
}