using SoulRunner.Fireball;
using SoulRunner.Utility.Spine;
using UnityEngine;

namespace SoulRunner.Configuration.Anim
{
  [CreateAssetMenu(fileName = "Anim", menuName = "SoulRunner/Anim/FireballAnim")]
  public class FireballAnim : ScriptableObject
  {
    public ConfigurableSpineAnimation<FireballAnimType>[] Anims;
  }
}