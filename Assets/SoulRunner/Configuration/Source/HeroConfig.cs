using System;
using UnityEngine;

namespace SoulRunner.Player
{
  [Serializable]
  public class HeroConfig
  {
    [Header("General")]
    public float MoveSpeed;
    public float JumpForce;
    
    [Header("Kelli")]
    public float FireDelay;
    public float FireTime;
    public float DashSpeed;
    public LayerMask DefaultLayer;
    public LayerMask DashLayer;
    public float DashDuration;
    public float DashDelay;
  }
}