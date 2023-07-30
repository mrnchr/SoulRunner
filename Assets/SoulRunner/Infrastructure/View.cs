using UnityEngine;

namespace SoulRunner
{
  public abstract class View : MonoBehaviour
  {
    public bool IsEcsActive { get; private set; }

    private int _entity;
    public int Entity
    {
      get => _entity;
      set
      {
        IsEcsActive = true;
        _entity= value;
      }
    }
  }
}