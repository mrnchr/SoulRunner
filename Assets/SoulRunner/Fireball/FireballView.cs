using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using UnityEngine;
using Zenject;

namespace SoulRunner.Fireball
{
  public class FireballView : View
  {
    public Rigidbody2D Rb;
    public FireballAnimator Anim;
    public ObstacleChecker ObstacleChecker;
    public FireballActionMachine ActionMachine;
    public FireballActionVariables ActionVariables = new FireballActionVariables();
    public WeaponCollider Weapon;
    [HideInInspector] public FireballSpec Spec;

    [Inject]
    public void Construct(ISpecificationService specSvc)
    {
      Spec = specSvc.GetSpec<FireballSpec>();
    }
    
    private void Reset()
    {
      TryGetComponent(out Rb);
      TryGetComponent(out Anim);
    }
  }
}