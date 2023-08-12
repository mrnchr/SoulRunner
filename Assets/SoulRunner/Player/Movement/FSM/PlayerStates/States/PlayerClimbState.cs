using SoulRunner.Control;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerClimbState : PlayerState
  {
    public override bool CanStart() => true;

    public override void Start()
    {
      StartClimb();
    }

    public override void End()
    {
      View.Rb.gravityScale = 1;
      
      Variables.IsClimbing = false;
      Variables.OnClimbEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      
      if(inputs.ClimbUpButton)
        Machine.ChangeState(PlayerStateType.Jump);
      else if (inputs.ClimbDownButton)
        Machine.ChangeState(Variables.IsOnGround ? PlayerStateType.Stand : PlayerStateType.Fall);
    }
    
    protected virtual void StartClimb()
    {
      GlueToLedge(Variables.LedgePosX);
      View.Rb.gravityScale = 0;
      View.Rb.velocity = Vector2.zero;

      Variables.IsClimbing = true;
      Variables.OnClimbStart?.Invoke();
    }

    protected virtual void GlueToLedge(float posX)
    {
      Vector3 pos = View.transform.position;
      pos.x += posX - View.LedgeChecker.transform.position.x;
      View.transform.position = pos;
    }
  }
}