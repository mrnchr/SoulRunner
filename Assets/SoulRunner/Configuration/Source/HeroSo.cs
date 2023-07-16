using SoulRunner.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "Character", menuName = "SR/Character/Character")]
  public class HeroSo : ScriptableObject
  {
    [FormerlySerializedAs("character")] public Hero hero;
  }
}