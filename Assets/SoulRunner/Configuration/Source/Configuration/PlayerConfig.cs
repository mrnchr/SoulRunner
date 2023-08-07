using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Configuration
{
  [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SoulRunner/Character/Player")]
  public class PlayerConfig : Config
  {
    [Header("General")]
    public float Health;
    public float BaseDamage;
    public float BaseAttackRatio;
    public float MoveSpeed;
    public float JumpForce;
    public float SwapDelay;
    public ObjectType StartHero;
    
    [Header("Kelli")]
    public float FireDelay;
    public float FireballSpeed;
    public float DashSpeed;
    public float DashDelay;
    public float KelliAttackDelay;
    public float KelliAttackRatio;

    [Header("Shon")]
    public float ShonAttackRatio;
  }
}