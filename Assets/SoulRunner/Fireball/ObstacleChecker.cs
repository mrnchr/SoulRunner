using System;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class ObstacleChecker : MonoBehaviour
  {
    public Action OnCollided;

    public LayerMask ObstacleMask;

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (ObstacleMask.Contains(other.gameObject.layer))
      {
        OnCollided?.Invoke();
      }
    }
  }
}