using SoulRunner.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "PrefabData", menuName = "SoulRunner/Data/Prefab")]
  public class PrefabData : ScriptableObject
  {
    public PlayerView Player;
  }
}