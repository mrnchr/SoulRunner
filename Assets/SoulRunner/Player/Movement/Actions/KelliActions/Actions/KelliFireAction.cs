using System;
using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class KelliFireAction : PlayerCycleAction, IFireAction, IUpdateAction, IKelliAction
  {
    private readonly PlayerConfig _playerCfg;
    private HandType _fireHand;

    public KelliFireAction(PlayerView view) : base(view)
    {
      _playerCfg = view.PlayerCfg;
    }

    public void Fire(HandType hand)
    {
      if (!IsActive || _variables.FireDelay > 0 || _variables.IsDashing) return;

      if (_variables.IsClimbing)
      {
        FireForcefully(HandType.Right);
        return;
      }

      if (_variables.NextHand != HandType.None && _variables.NextHand != hand) return;
      
      _variables.NextHand = GetNextHand(hand);
      FireForcefully(hand);
    }

    public void FireForcefully(HandType hand)
    {
      _variables.IsFiring = true;
      _fireHand = hand;
      _variables.OnFireStart?.Invoke(_fireHand);
      TimerManager.AddTimer(_variables.BeforeFire = _playerCfg.BeforeFireTime);
      TimerManager.AddTimer(_variables.FireDelay = _playerCfg.FireDelay);
    }

    public void Update()
    {
      if (_variables.IsFiring && _variables.BeforeFire <= 0)
      {
        _variables.OnFire?.Invoke(_fireHand);
        _variables.IsFiring = false;
      }
    }

    public override void Deactivate()
    {
      base.Deactivate();
      _variables.IsFiring = false;
    }

    private HandType GetNextHand(HandType hand) =>
      hand switch
      {
        HandType.Left  => HandType.Right,
        HandType.Right => HandType.Left,
        _              => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
      };
  }
  
}