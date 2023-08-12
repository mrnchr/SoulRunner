using SoulRunner.Control;
using SoulRunner.Infrastructure.FSM;

namespace SoulRunner.Player
{
  public abstract class PlayerState : State<PlayerView, PlayerStateType>, IStartState
  {
    public PlayerStateMachine Machine;
    public PlayerStateVariables Variables;

    public virtual void OnStart()
    {
      Variables = Machine.Variables;
    }
    
    public abstract void ProcessInput(InputValues inputs);
  }
}