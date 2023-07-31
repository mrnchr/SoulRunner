using System;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class KelliFireAction : PlayerMovementAction, IFireAction, IUpdateAction, IKelliAction
  {
    private HandType _fireHand;

    public KelliFireAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public void Fire(HandType hand)
    {
      if (!IsActive || _chars.FireDelay.Current > 0 || _variables.IsDashing) return;

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
      TimerManager.AddTimer(_variables.BeforeFire = _spec.BeforeFireTime);
      TimerManager.AddTimer(_chars.FireDelay.Current = _chars.FireDelay.Max);
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
      TimerManager.RemoveTimer(_variables.BeforeFire);
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