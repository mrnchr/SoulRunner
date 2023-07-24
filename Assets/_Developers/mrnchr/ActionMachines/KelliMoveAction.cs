using System;
using UnityEngine;

namespace SoulRunner.Player.ActionMachines
{
  public class KelliMoveAction : MovementAction<PlayerView>, IMoveAction
  {
    public Action<float> OnStart { get; set; } 
    
    private readonly Rigidbody2D _rb;
    private readonly PlayerConfig _playerCfg;

    public KelliMoveAction(PlayerView view) : base(view)
    {
      _rb = view.Rb;
      _playerCfg = view.PlayerCfg;
    }
    
    public void Move(float direction)
    {
      _rb.velocity = new Vector2(_playerCfg.MoveSpeed * direction, _rb.velocity.y);
      OnStart?.Invoke(direction);
    }
  }
}