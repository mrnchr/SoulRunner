using SoulRunner.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "Character", menuName = "SoulRunner/Character/Character")]
  public class HeroSo : ScriptableObject
  {
    [FormerlySerializedAs("Hero"),FormerlySerializedAs("hero"),FormerlySerializedAs("character")] public HeroConfig HeroCfg;
  }
}