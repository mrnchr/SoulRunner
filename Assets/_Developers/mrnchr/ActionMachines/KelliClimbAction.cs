namespace SoulRunner.Player.ActionMachines
{
  public class ClimbAction : MovementAction<PlayerView>, IClimbAction
  {
    private readonly KelliActionMachine _machine;

    public ClimbAction(PlayerView view) : base(view)
    {
      _machine = view.KelliMachine;
    }

    public void Climb()
    {
      if (!_machine.IsOnLedge) return;
      if (!_machine.IsClimbing && !_machine.IsCrouching && !_machine.IsJumping && !_machine.IsFalling)
      {
        StartClimb();
      }
    }
    
    private void StartClimb()
    {
      
    }
  }
}