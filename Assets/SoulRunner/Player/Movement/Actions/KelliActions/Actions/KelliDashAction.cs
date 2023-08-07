using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliDashAction : PlayerMovementAction, IDashAction, IStartAction, IUpdateAction, IKelliAction
  {
    private Timer _dashHold = 0;
    private ILandAction _land;

    public KelliDashAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public void Start()
    {
      _land = _machine.GetAction<ILandAction>();
    }

    public void Dash()
    {
      if (!IsActive || _chars.DashDelay.Current > 0 || _variables.IsCrouching || _variables.IsClimbing || _variables.IsAttacking) return;
      
      _view.gameObject.layer = _view.DashLayer.GetLayerIndex();
      _view.Rb.gravityScale = 0;
      _view.Rb.velocity = Vector2.right * (_view.transform.localScale.x * _chars.DashSpeed);
      _land.Land();
      
      _variables.IsDashing = true;
      _variables.OnDashStart?.Invoke();
      _chars.DashDelay.ToDefault();
      TimerManager.AddTimer(_dashHold = _spec.DashDuration);
      TimerManager.AddTimer(_chars.DashDelay.Current);
    }

    public void StopDashing()
    {
      _view.gameObject.layer = _view.DefaultLayer.GetLayerIndex();
      _view.Rb.gravityScale = 1;
      _view.Rb.velocity = Vector2.zero;
      
      _variables.IsDashing = false;
      _variables.OnDashEnd?.Invoke();
    }

    public void Update()
    {
      if (_variables.IsDashing && _dashHold <= 0)
        StopDashing();
    }
  }
}