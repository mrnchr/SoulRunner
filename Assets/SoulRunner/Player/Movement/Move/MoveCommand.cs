using System;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  [Serializable]
  public struct MoveCommand
  {
    [FormerlySerializedAs("direction")] public float Direction;
  }
}