using System;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  [Serializable]
  public class HeroConfig
  {
    [FormerlySerializedAs("moveSpeed")] public float MoveSpeed;
    [FormerlySerializedAs("jumpForce")] public float JumpForce;
    public float HoldFireTime;
    public float FireTime;
  }
}