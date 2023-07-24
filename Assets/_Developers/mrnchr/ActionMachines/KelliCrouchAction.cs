using System;
using System.Collections;
using UnityEngine;

namespace SoulRunner.Player.ActionMachines
{
  public class KelliCrouchAction : MovementAction<PlayerView>, ICrouchAction, IStandAction, IStartAction, IEndAction
  {
    public Action OnStart { get; set; }
    public Action OnEnd { get; set; }

    private readonly ActionMachine _machine;
    private bool _isCrouching;
    
    public KelliCrouchAction(PlayerView view) : base(view)
    {
      // _machine = view.CurrentMachine;
    }

    public void Crouch()
    {
      _isCrouching = true;
      if (!_machine.IsOnGround && _machine.IsCrouch) return;

      _view.StayCollider.enabled = false;
      _view.CrouchCollider.enabled = true;
      _view.Rb.velocity = Vector2.zero;

      _machine.IsCrouch = true;

      _machine.StartCoroutine(WaitForStand());
      OnStart.Invoke();
    }

    public void Stand()
    {
      _view.CrouchCollider.enabled = false;
      _view.StayCollider.enabled = true;
      _machine.IsCrouch = false;
      OnEnd.Invoke();
    }

    private IEnumerator WaitForStand()
    {
      while (_isCrouching)
      {
        _isCrouching = false;
        yield return new WaitForEndOfFrame();
      }

      Stand();
    }
  }
}