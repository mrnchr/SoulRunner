using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerCrouchAction : PlayerMovementAction, ICrouchAction, IStandAction, IUpdateAction
  {
    protected bool _isCrouchCommand;

    public PlayerCrouchAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Crouch()
    {
      if (!IsActive) return;
      _isCrouchCommand = true;
      if (!_variables.IsOnGround || _variables.IsCrouching || _variables.IsJumping || _variables.IsClimbing ||
        _variables.IsFalling || _variables.IsDashing) return;
      _view.Rb.velocity = Vector2.zero;

      _view.StayCollider.enabled = false;
      _view.CrouchCollider.enabled = true;

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
      if (!IsActive) return;
      if (_variables.IsCrouching)
      {
        _view.Rb.velocity = new Vector2(0, _view.Rb.velocity.y);
        if (!_isCrouchCommand)
          Stand();

        _isCrouchCommand = false;
      }
    }
  }
}