using SoulRunner.Infrastructure.FSM;

namespace SoulRunner.Fireball
{
  public abstract class FireballState : State<FireballView, FireballStateType>, IStartState, IUpdateState
  {
    public FireballStateMachine Machine;
    public FireballStateVariables Variables;

    public virtual void OnStart()
    {
      Variables = View.StateVariables;
    }


    public abstract void Update();
  }
}