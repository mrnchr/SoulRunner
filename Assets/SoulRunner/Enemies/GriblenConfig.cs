using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Enemies
{
  [CreateAssetMenu(fileName = "GriblenConfig", menuName = "SoulRunner/Enemy/Griblen")]
  public class GriblenConfig : Config
  {
    public float Health;
  }
}