using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "PlayerSpecification", menuName = "SoulRunner/Specifications/Player")]
  public class PlayerSpec : Specification
  {
    public float BeforeFireTime;
    public float DashDuration;
  }
}