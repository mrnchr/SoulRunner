using System;
using UnityEngine;

namespace SoulRunner.Player
{
  public class GroundChecker : MonoBehaviour
  {
    public Action OnGroundEnter;
    public Action OnGroundExit;

    public LayerMask Ground;

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
      if (((1 << layer) & Ground) > 0)
        _isOnGround = true;
    }
  }
}