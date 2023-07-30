using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "Character", menuName = "SoulRunner/Character/Character")]
  public class HeroSo : ScriptableObject
  {
    public PlayerConfig PlayerCfg;
  }
}