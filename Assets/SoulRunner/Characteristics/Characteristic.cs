using System;
using UnityEngine;

namespace SoulRunner.Characteristics
{
  [Serializable]
  public class Characteristic<T> : ICharacteristic, IDefaultChar
  {
    public T Default
    {
      get => _default;
      set => SetDefault(value);
    }

    public T Current
    {
      get => _current;
      set => SetCurrent(value);
    }

    [SerializeField] protected T _current;
    [SerializeField] protected T _default;

    public virtual void Init() => ToDefault();
    public virtual void ToDefault() => _current = Default;
    public static implicit operator T(Characteristic<T> obj) => obj.Current;

    protected virtual void SetDefault(T value) => _default = value;
    protected virtual void SetCurrent(T value) => _current = value;
  }
}