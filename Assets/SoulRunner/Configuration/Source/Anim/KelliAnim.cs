using SoulRunner.Infrastructure.Spine;
using SoulRunner.Player;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "Anim", menuName = "SoulRunner/Anim/KelliAnim", order = 1)]
  public class KelliAnim : ScriptableObject
  {
    public ConfigurableSpineAnimation<KelliAnimType>[] Anims;
  }
}