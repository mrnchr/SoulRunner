using SoulRunner.Fireball;
using SoulRunner.Infrastructure.Spine;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "Anim", menuName = "SoulRunner/Anim/FireballAnim")]
  public class FireballAnim : ScriptableObject
  {
    public ConfigurableSpineAnimation<FireballAnimType>[] Anims;
  }
}