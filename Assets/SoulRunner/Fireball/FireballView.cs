using SoulRunner.Player;
using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballView : View
  {
    public Rigidbody2D Rb;
    public FireballAnimator Anim;

    private void Reset()
    {
      TryGetComponent(out Rb);
      TryGetComponent(out Anim);
    }
  }
}