using UnityEngine;

namespace SoulRunner.Utility
{
  public static class UnityExtensions
  {
    public static int GetLayerIndex(this LayerMask obj)
    {
      int index = 0;
      int layer = obj.value;
      while (layer > 1)
      {
        layer >>= 1;
        ++index;
      }
      
      return index;
    }
  }
}