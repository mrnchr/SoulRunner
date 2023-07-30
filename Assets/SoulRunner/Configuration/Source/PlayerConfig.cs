using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Configuration
{
  [Serializable]
  public class PlayerConfig
  {
    [Header("General")]
    public float MoveSpeed;
    public float JumpForce;
    public float SwapDelay;
    
    [Header("Kelli")]
    public float FireDelay;
    [FormerlySerializedAs("FireTime")] public float BeforeFireTime;
    public float DashSpeed;
    public float DashDuration;
    public float DashDelay;
  }
}