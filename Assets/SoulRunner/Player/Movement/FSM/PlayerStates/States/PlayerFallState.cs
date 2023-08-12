using SoulRunner.Control;

namespace SoulRunner.Player
{
  public class PlayerFallState : PlayerState
  {
    public override bool CanStart() => true;

    public override void Start()
    {
      Variables.IsFalling = true;
      Variables.OnFallStart?.Invoke();
    }

    public override void End()
    {
      Variables.IsFalling = false;
      Variables.OnFallEnd?.Invoke();
    }

    public override void ProcessInput(InputValues inputs)
    {
      if (Variables.IsOnGroundEnter)
        Machine.ChangeState(PlayerStateType.Stand);
      else if (Variables.IsOnLedge && (Machine.OldState != PlayerStateType.Climb || inputs.GrabButton))
        Machine.ChangeState(PlayerStateType.Climb);
    }
  }
}