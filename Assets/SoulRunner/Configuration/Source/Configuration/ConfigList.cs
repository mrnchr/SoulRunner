using System.Collections.Generic;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "ConfigList", menuName = "SoulRunner/ConfigList")]
  public class ConfigList : ScriptableObject
  {
    public List<Config> Configs;
  }
}