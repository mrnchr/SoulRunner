using SoulRunner.Player;
using SoulRunner.Utility.Spine;
using UnityEngine;

namespace SoulRunner.Configuration.Anim
{
  [CreateAssetMenu(fileName = "Anim", menuName = "SoulRunner/Anim/KelliAnim", order = 1)]
  public class KelliAnim : ScriptableObject
  {
    public ConfigurableSpineAnimation<KelliAnimType>[] Anims;
  }
}