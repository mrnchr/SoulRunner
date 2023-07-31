using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliDashAction : PlayerMovementAction, IDashAction, IUpdateAction, IKelliAction
  {
    private Timer _dashHold = 0;

    public KelliDashAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public void Dash()
    {
      if (!IsActive || _chars.DashDelay.Current > 0 || _variables.IsCrouching || _variables.IsClimbing) return;
      
      _view.gameObject.layer = _view.DashLayer.GetLayerIndex();
      _view.Rb.gravityScale = 0;
      _view.Rb.velocity = Vector2.right * (_view.transform.localScale.x * _chars.DashSpeed);

      _variables.IsDashing = true;
      _variables.OnDashStart?.Invoke();
      TimerManager.AddTimer(_dashHold = _spec.DashDuration);
      TimerManager.AddTimer(_chars.DashDelay.Current = _chars.DashDelay.Max);
    }

    public void StopDashing()
    {
      _view.gameObject.layer = _view.DefaultLayer.GetLayerIndex();
      _view.Rb.velocity = Vector2.zero;
      _view.Rb.gravityScale = 1;
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