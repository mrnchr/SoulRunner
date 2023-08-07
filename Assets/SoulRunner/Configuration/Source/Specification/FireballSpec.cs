using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "FireballSpec", menuName = "SoulRunner/Items/FireballSpec")]
  public class FireballSpec : Specification
  {
    public float DeathDuration;
  }
}