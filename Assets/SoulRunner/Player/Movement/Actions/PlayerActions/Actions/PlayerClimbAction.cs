using SoulRunner.Infrastructure.Actions;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerClimbAction : PlayerMovementAction, IClimbAction, IStartAction, IUpdateAction
  {
    protected ILandAction _land;

    public PlayerClimbAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public virtual void Start()
    {
      _land = _machine.GetAction<ILandAction>();
    }

    public virtual void Climb()
    {
      if (!IsActive || !_variables.IsOnLedge || _variables.IsAttacking) return;
      if (_variables.IsFalling)
      {
        _land.Land();
        StartClimb();
      }
    }

    public virtual void Update()
    {
      if (!IsActive || !_variables.IsOnLedge || _variables.IsAttacking) return;
      if (!_variables.IsClimbing && !_variables.IsCrouching && !_variables.IsJumping && !_variables.IsFalling && !_variables.IsDashing)
      {
        StartClimb();
      }

      if (_variables.IsJumping && _view.Rb.velocity.y < 0)
      {
        _land.Land();
        StartClimb();
      }
    }

    protected virtual void StartClimb()
    {
      GlueToLedge(_variables.LedgePosX);
      _view.Rb.gravityScale = 0;
      _view.Rb.velocity = Vector2.zero;

      _variables.IsClimbing = true;
      _variables.OnClimbStart?.Invoke();
    }

    protected virtual void GlueToLedge(float posX)
    {
      Vector3 pos = _view.transform.position;
      pos.x += posX - _view.LedgeChecker.transform.position.x;
      _view.transform.position = pos;
    }
  }
}