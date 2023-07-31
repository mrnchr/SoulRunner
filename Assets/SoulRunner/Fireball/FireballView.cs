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
    public FireballConfig FireballCfg;
    public ObstacleChecker ObstacleChecker;
    public FireballActionMachine ActionMachine;
    public FireballActionVariables ActionVariables;

    [Inject]
    public void Construct(IConfigService configSvc)
    {
      FireballCfg = configSvc.GetConfig<FireballConfig>();
    }
    
    private void Reset()
    {
      TryGetComponent(out Rb);
      TryGetComponent(out Anim);
    }
  }
}