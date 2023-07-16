using SoulRunner.Player;
using SoulRunner.Utility.Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Configuration.Anim
{
  [CreateAssetMenu(fileName = "Anim", menuName = "SoulRunner/Anim/KelliAnim", order = 1)]
  public class KelliAnim : ScriptableObject
  {
    public ConfigurableSpineAnimation<KelliAnimType>[] Anims;
  }
}