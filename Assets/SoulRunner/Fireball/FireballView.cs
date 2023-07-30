using SoulRunner.Configuration;
using SoulRunner.Configuration.Source;
using SoulRunner.Player;
using UnityEngine;
using Zenject;

namespace SoulRunner.Fireball
{
  public class FireballView : View
  {
    public Rigidbody2D Rb;
    public FireballAnimator Anim;
    public FireballConfig FireballCfg;

    [Inject]
    public void Construct(FireballConfig fireballCfg)
    {
      FireballCfg = fireballCfg;
    }
    
    private void Reset()
    {
      TryGetComponent(out Rb);
      TryGetComponent(out Anim);
    }
  }
}