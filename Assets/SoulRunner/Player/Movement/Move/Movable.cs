using System;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  [Serializable]
  public struct Movable
  {
    [FormerlySerializedAs("speed")] public float Speed;
  }
}