using System;
using SoulRunner.Infrastructure.FSM;
using SoulRunner.Utility;

namespace SoulRunner.Fireball
{
  public class FireballStateMachine : FinalStateMachine<FireballView, FireballStateType>
  {
    public FireballStateVariables Variables;
    public ObstacleChecker Obstacle;
    
    private void Awake()
    {
      Variables = View.StateVariables;
      _states
        .AddItem(new FireballAliveState().Init(FireballStateType.Alive))
        .AddItem(new FireballDeadState().Init(FireballStateType.Dead));
      
      foreach (var state in GetRawStates<FireballState>())
        InitState(state);
      
      ReplaceState(FireballStateType.Alive);
    }

    private void InitState(FireballState state)
    {
      state.Machine = this;
      state.View = View;
    }

    private void OnEnable()
    {
      Obstacle.OnCollided += () => Variables.IsCollided = true;
    }
    
    private void OnDisable()
    {
      Obstacle.OnCollided -= () => Variables.IsCollided = true;
    }
  }
}