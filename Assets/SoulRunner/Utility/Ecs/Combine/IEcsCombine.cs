namespace SoulRunner.Utility.Ecs.Combine
{
  public interface IEcsCombine
  {
    public EcsCombine Combine();
    public EcsCombine Add(IEcsEngine engine);
    public void Run();
    public void Destroy();
  }
}