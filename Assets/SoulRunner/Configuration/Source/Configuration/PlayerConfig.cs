using SoulRunner.Player;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SoulRunner/Character/Player")]
  public class PlayerConfig : Config
  {
    [Header("General")]
    public float MoveSpeed;
    public float JumpForce;
    public float SwapDelay;
    public HeroType StartHero;
    
    [Header("Kelli")]
    public float FireDelay;
    public float BeforeFireTime;
    public float DashSpeed;
    public float DashDuration;
    public float DashDelay;
  }
}