using SoulRunner.Control;
using SoulRunner.Infrastructure.FSM;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerCrouchState : PlayerState, IUpdateState
  {
    public override bool CanStart() => true;

    public override void Start()
    {
      View.Rb.velocity = Vector2.zero;

      View.StayCollider.enabled = false;
      View.CrouchCollider.enabled = true;

      Variables.IsCrouching = true;
      Variables.OnCrouchStart?.Invoke();
    }

    public override void End()
    {
      View.CrouchCollider.enabled = false;
      View.StayCollider.enabled = true;
      Variables.IsCrouching = false;
      Variables.OnCrouchEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (!inputs.CrouchButton)
        Machine.ChangeState(PlayerStateType.Stand);
    }

    public void Update()
    {
      View.Rb.velocity = new Vector2(0, View.Rb.velocity.y);
    }
  }
}