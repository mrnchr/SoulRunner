using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "Fireball", menuName = "SoulRunner/Items/Fireball")]
  public class FireballConfig : Config
  {
    public float Speed;
    public float DeathDuration;
  }
}