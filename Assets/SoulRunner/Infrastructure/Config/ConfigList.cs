using System.Collections.Generic;
using UnityEngine;

namespace SoulRunner.Infrastructure
{
  [CreateAssetMenu(fileName = "ConfigList", menuName = "SoulRunner/ConfigList")]
  public class ConfigList : ScriptableObject
  {
    public List<Config> Configs;
  }
}