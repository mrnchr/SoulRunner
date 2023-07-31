namespace SoulRunner.Infrastructure.Actions
{
  public interface ICycleAction
  {
    public bool IsActive { get; set; }
    public void Activate();
    public void Deactivate();
  }
}