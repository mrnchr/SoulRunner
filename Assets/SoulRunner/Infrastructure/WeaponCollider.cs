using UnityEngine;

namespace SoulRunner.Infrastructure
{
  public class WeaponCollider : MonoBehaviour
  {
    public View Owner;
    public float AttackPoints;

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (!other.otherCollider.isTrigger && other.gameObject.TryGetComponent(out AttackSystem attackSys))
      {
        attackSys.TakeDamage(Owner, AttackPoints);
      }
    }
  }
}