using System;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class ObstacleChecker : MonoBehaviour
  {
    public Action OnCollided;

    public LayerMask ObstacleMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (!collision.otherCollider.isTrigger && ObstacleMask.Contains(collision.gameObject.layer))
        OnCollided?.Invoke();
    }
  }
}