using System;
using SoulRunner.LevelManagement;
using SoulRunner.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  public class LedgeChecker : MonoBehaviour
  {
    public Action<float> OnLedgeEnter;
    public Action OnLedgeExit;

    public LayerMask LedgeMask;

    private Collider2D _ledge;
    private bool _isOnLedge;
    private bool _wasOnLedge;

    private void FixedUpdate()
    {
      if (_isOnLedge != _wasOnLedge)
      {
        if(_isOnLedge)
          OnLedgeEnter?.Invoke(_ledge.GetComponent<Ledge>().PosX);
        else
          OnLedgeExit?.Invoke();
      }

      _wasOnLedge = _isOnLedge;
      _isOnLedge = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (CheckOnLedge(other.gameObject.layer))
        _ledge = other;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (CheckOnLedge(other.gameObject.layer))
        _ledge = other;
    }

    private bool CheckOnLedge(int layer)
    {
      if (!LedgeMask.Contains(layer)) return false;
      _isOnLedge = true;
      return true;
    }
  }
}