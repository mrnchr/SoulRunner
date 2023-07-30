using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunner.Infrastructure.Actions
{
  public class ActionMachine<TView> : MonoBehaviour
  where TView : View
  {
    [SerializeField] protected TView _view;
    protected readonly List<MovementAction<TView>> _actions = new List<MovementAction<TView>>();

    public T GetAction<T>() => GetActions<T>()[0];
    public T2 GetAction<T1, T2>() => GetActions<T1, T2>()[0];
    public T3 GetAction<T1, T2, T3>() => GetActions<T1, T2, T3>()[0];
    public T4 GetAction<T1, T2, T3, T4>() => GetActions<T1, T2, T3, T4>()[0];

    public T[] GetActions<T>() => GetRawActions<T>().ToArray();
    public T2[] GetActions<T1, T2>() => GetRawActions<T1, T2>().ToArray();
    public T3[] GetActions<T1, T2, T3>() => GetRawActions<T1, T2, T3>().ToArray();
    public T4[] GetActions<T1, T2, T3, T4>() => GetRawActions<T1, T2, T3, T4>().ToArray();

    protected IEnumerable<T> GetRawActions<T>() => _actions.OfType<T>();
    protected IEnumerable<T2> GetRawActions<T1, T2>() => GetRawActions<T1>().OfType<T2>();
    protected IEnumerable<T3> GetRawActions<T1, T2, T3>() => GetRawActions<T1, T2>().OfType<T3>();
    protected IEnumerable<T4> GetRawActions<T1, T2, T3, T4>() => GetRawActions<T1, T2, T3>().OfType<T4>();
    
    protected virtual void Start()
    {
      foreach (IStartAction action in GetActions<IStartAction>())
        action.Start();
    }
    
    protected virtual void Update()
    {
      foreach (IUpdateAction action in GetRawActions<IUpdateAction>())
        action.Update();
    }
  }
}