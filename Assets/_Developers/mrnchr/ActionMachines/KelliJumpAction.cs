using System;
using System.Collections;
using UnityEngine;

namespace SoulRunner.Player.ActionMachines
{
  public class KelliJumpAction : MovementAction<PlayerView>, IJumpAction, ILandAction, IStartAction, IEndAction
  {
    public Action OnStart { get; set; }
    public Action OnEnd { get; set; }
    
    private readonly ActionMachine _machine;
    private readonly Rigidbody2D _rb;
    private readonly PlayerConfig _playerCfg;

    public KelliJumpAction(PlayerView view) : base(view)
    {
      // _machine = view.CurrentMachine;
      _rb = view.Rb;
      _playerCfg = view.PlayerCfg;
    }

    public void Jump()
    {
      if (!_machine.IsOnGround) return;

      _rb.velocity = new Vector2(_rb.velocity.x, 0);
      _rb.AddForce(Vector2.up * _playerCfg.JumpForce, ForceMode2D.Impulse);

      _machine.IsOnGround = false;
      _machine.IsInJump = true;
      
      _machine.StartCoroutine(WaitForLand());
      OnStart?.Invoke();
    }

    public void Land()
    {
      _machine.IsInJump = false;
      OnEnd?.Invoke();
    }

    private IEnumerator WaitForLand()
    {
      yield return new WaitUntil(() => _machine.IsOnGround);
      Land();
    }
  }
}