namespace SoulRunner.Player
{
  public class PlayerCycleAction : PlayerMovementAction, ICycleAction
  {
    public bool IsActive { get; set; }
    
    public PlayerCycleAction(PlayerView view) : base(view)
    {
    }

    public virtual void Activate()
    {
      IsActive = true;
    }

    public virtual void Deactivate()
    {
      IsActive = false;
    }
  }
}