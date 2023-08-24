using System;
using SoulRunner.Control;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.FSM;
using static SoulRunner.Player.PlayerStateType;

namespace SoulRunner.Player
{
  public class KelliFireState : PlayerState, IUpdateState
  {
    private HandType _fireHand;
    private Timer _beforeFire = 0;
    private bool _isFiring;
    
    public override bool CanStart() => true;

    public override void Start()
    {
    }

    public override void End()
    {
      StopFiring();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (inputs.SwapHeroButton)
      {
        StopFiring();
        return;
      }
      
      bool fire = inputs.FireLeftButton || inputs.FireRightButton;
      if (!fire || View.Chars.FireDelay.Current > 0 || View.Chars.Energy < View.Chars.GetEnergyCost() ||
        Machine.CurrentState is MainAbility or SuperAttack or SideAbility) return;

      if (Machine.CurrentState == Climb)
      {
        FireForcefully(HandType.Right);
        return;
      }
      
      _fireHand = inputs.FireLeftButton ? HandType.Left : HandType.Right;

      if (Variables.NextHand != HandType.None && Variables.NextHand != _fireHand) return;
      Variables.NextHand = GetNextHand(_fireHand);
      FireForcefully(_fireHand);
    }

    public void FireForcefully(HandType hand)
    {
      _isFiring = true;
      _fireHand = hand;
      Variables.OnFireStart?.Invoke(_fireHand);
      View.Chars.FireDelay.ToDefault();
      TimerManager.AddTimer(_beforeFire = View.Spec.BeforeFireTime);
      TimerManager.AddTimer(View.Chars.FireDelay.Current);
    }

    public void Update()
    {
      if (_isFiring && _beforeFire <= 0)
      {
        Variables.OnFire?.Invoke(_fireHand);
        _isFiring = false;
      }
    }

    private void StopFiring()
    {
      if (!_isFiring) return;
      Variables.IsFiring = false;
      TimerManager.RemoveTimer(_beforeFire);
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