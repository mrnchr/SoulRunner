using UnityEngine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public abstract class AttackSystem : MonoBehaviour, IAttackSystem
  {
    protected IDamageService _damageSvc;

    [Inject]
    public virtual void Construct(IDamageService damageSvc)
    {
      _damageSvc = damageSvc;
    }
    
    public abstract float GetDamagePoints();
    public abstract float TakeDamage(View damaging, float damagePoints);
    
  }
}