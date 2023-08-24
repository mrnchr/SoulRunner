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
    public float Energy;
    public float BaseEnergyRatio;
    public float EnergyResetRatio;
    public float EnergyResetSpeed;
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
    public float KelliEnergyCost;

    [Header("Shon")]
    public float ShonAttackRatio;
    public float ShonEnergyCost;
  }
}