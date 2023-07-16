using SoulRunner.Fireball;
using SoulRunner.Player;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "PrefabData", menuName = "SoulRunner/Data/Prefab")]
  public class PrefabData : ScriptableObject
  {
    public PlayerView Player;
    public FireballView Fireball;
  }
}