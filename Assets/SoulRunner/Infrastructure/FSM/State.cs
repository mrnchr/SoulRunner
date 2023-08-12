namespace SoulRunner.Infrastructure.FSM
{
  public abstract class State<TView, TEnum> : IState
    where TView : View
    where TEnum : System.Enum
  {
    public TEnum Type;
    public TView View;

    public State<TView, TEnum> Init(TEnum type)
    {
      Type = type;
      return this;
    }

    public abstract bool CanStart();
    public abstract void Start();
    public abstract void End();
  }

  public interface IState
  {
    public bool CanStart();
    public void Start();
    public void End();
  }
}