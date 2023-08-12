using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunner.Infrastructure.FSM
{
  public abstract class FinalStateMachine<TView, TEnum> : MonoBehaviour
    where TView : View
    where TEnum : System.Enum
  {
    public TEnum OldState
    {
      get => _oldState;
      private set => _oldState = value;
    }

    public TEnum CurrentState
    {
      get => _currentState;
      private set => _currentState = value;
    }

    public TView View;
    protected readonly List<State<TView, TEnum>> _states = new List<State<TView, TEnum>>();
    protected IUpdateState _updated;

    [SerializeField] private TEnum _oldState;
    [SerializeField] private TEnum _currentState;

    public virtual void ChangeState(TEnum newState)
    {
      if (!GetState<State<TView, TEnum>>(newState).CanStart()) return;
      
      OldState = CurrentState;
      CurrentState = newState;

      State<TView, TEnum> current = GetState<State<TView, TEnum>>(CurrentState);
      
      GetState<State<TView, TEnum>>(OldState).End();
      current.Start();
      _updated = current as IUpdateState;
    }

    public virtual void ReplaceState(TEnum newState)
    {
      OldState = CurrentState;
      CurrentState = newState;

      _updated = GetState<State<TView, TEnum>>(CurrentState) as IUpdateState;
    }

    public State<TView, TEnum> GetState(TEnum type) => GetState<State<TView, TEnum>>(type);

    public T GetState<T>(TEnum type)
      where T : State<TView, TEnum> 
      => (T) _states.Single(x => x.Type.Equals(type));

    public T GetState<T>() => GetRawStates<T>().Single();
    public T2 GetState<T1, T2>() => GetRawStates<T1, T2>().Single();
    public T3 GetState<T1, T2, T3>() => GetRawStates<T1, T2, T3>().Single();
    public T4 GetState<T1, T2, T3, T4>() => GetRawStates<T1, T2, T3, T4>().Single();

    public T[] GetStates<T>() => GetRawStates<T>().ToArray();
    public T2[] GetStates<T1, T2>() => GetRawStates<T1, T2>().ToArray();
    public T3[] GetStates<T1, T2, T3>() => GetRawStates<T1, T2, T3>().ToArray();
    public T4[] GetStates<T1, T2, T3, T4>() => GetRawStates<T1, T2, T3, T4>().ToArray();

    protected IEnumerable<T> GetRawStates<T>() => _states.OfType<T>();
    protected IEnumerable<T2> GetRawStates<T1, T2>() => GetRawStates<T1>().OfType<T2>();
    protected IEnumerable<T3> GetRawStates<T1, T2, T3>() => GetRawStates<T1, T2>().OfType<T3>();
    protected IEnumerable<T4> GetRawStates<T1, T2, T3, T4>() => GetRawStates<T1, T2, T3>().OfType<T4>();

    protected virtual void Start()
    {
      foreach (IStartState state in GetStates<IStartState>())
        state.OnStart();
    }

    protected virtual void Update()
    {
      _updated?.Update();
    }
  }
}