using System;
using SoulRunner.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  public class GroundChecker : MonoBehaviour
  {
    public Action OnGroundEnter;
    public Action OnGroundExit;

    [FormerlySerializedAs("Ground")] public LayerMask GroundMask;

    private bool _isOnGround;
    private bool _wasOnGround;

    private void FixedUpdate()
    {
      if (_isOnGround != _wasOnGround)
      {
        if(_isOnGround)
          OnGroundEnter?.Invoke();
        else
          OnGroundExit?.Invoke();
      }

      _wasOnGround = _isOnGround;
      _isOnGround = false;
    }

    private void OnTriggerEnter2D(Collider2D other) => CheckOnGround(other.gameObject.layer);
    private void OnTriggerStay2D(Collider2D other) => CheckOnGround(other.gameObject.layer);

    private void CheckOnGround(int layer)
    {
      if (GroundMask.Contains(layer))
        _isOnGround = true;
    }
  }
}