using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Player
{
  public class KelliDashAction : PlayerCycleAction, IDashAction, IUpdateAction, IKelliAction
  {
    private readonly PlayerConfig _playerCfg;
    private Timer _dashHold = 0;

    public KelliDashAction(PlayerView view) : base(view)
    {
      _playerCfg = _view.PlayerCfg;
    }

    public void Dash()
    {
      if (!IsActive || _variables.DashDelay > 0 || _variables.IsCrouching || _variables.IsClimbing) return;
      
      _view.gameObject.layer = _view.DashLayer.GetLayerIndex();
      _view.Rb.gravityScale = 0;
      _view.Rb.velocity = Vector2.right * (_view.transform.localScale.x * _playerCfg.DashSpeed);

      _variables.IsDashing = true;
      _variables.OnDashStart?.Invoke();
      TimerManager.AddTimer(_dashHold = _playerCfg.DashDuration);
      TimerManager.AddTimer(_variables.DashDelay = _playerCfg.DashDelay);
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