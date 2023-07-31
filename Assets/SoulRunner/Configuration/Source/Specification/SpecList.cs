using System.Collections.Generic;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "SpecList", menuName = "SoulRunner/SpecList")]
  public class SpecList : ScriptableObject
  {
    public List<Specification> Specs;
  }
}