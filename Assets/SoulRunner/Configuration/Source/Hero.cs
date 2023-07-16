using System;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  [Serializable]
  public class Hero
  {
    [FormerlySerializedAs("moveSpeed")] public float MoveSpeed;
    [FormerlySerializedAs("jumpForce")] public float JumpForce;
  }
}