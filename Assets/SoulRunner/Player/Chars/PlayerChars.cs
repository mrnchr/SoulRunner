using System;
using SoulRunner.Characteristics;
using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using SoulRunner.Utility;

namespace SoulRunner.Player
{
  [Serializable]
  public sealed class PlayerChars : CharacteristicList<PlayerConfig>
  {
    public HeroChar Hero;
    public HealthChar Health;
    public BaseDamageChar BaseDamage;
    public BaseAttackRatioChar BaseAttackRatio;
    public HeroAttackRatioChar KelliAttackRatio;
    public HeroAttackRatioChar ShonAttackRatio;
    public MoveSpeedChar MoveSpeed;
    public JumpForceChar JumpForce;
    public FireDelayChar FireDelay;
    public FireballSpeedChar FireballSpeed;
    public DashSpeedChar DashSpeed;
    public DashDelayChar DashDelay;
    public AttackDelayChar KelliAttackDelay;
    public SwapDelayChar SwapDelay;

    public override CharMask<TChar> GetChars<TChar>() => new PlayerCharMask<TChar>(_chars);

    protected override void FillChars()
    {
      _chars
        .AddItem(Hero = new HeroChar())
        .AddItem(Health = new HealthChar())
        .AddItem(BaseDamage = new BaseDamageChar())
        .AddItem(BaseAttackRatio = new BaseAttackRatioChar())
        .AddItem(ShonAttackRatio = new HeroAttackRatioChar { Owner = ObjectType.Shon})
        .AddItem(KelliAttackRatio = new HeroAttackRatioChar { Owner = ObjectType.Kelli})
        .AddItem(MoveSpeed = new MoveSpeedChar())
        .AddItem(JumpForce = new JumpForceChar())
        .AddItem(FireDelay = new FireDelayChar())
        .AddItem(FireballSpeed = new FireballSpeedChar())
        .AddItem(DashSpeed = new DashSpeedChar())
        .AddItem(DashDelay = new DashDelayChar())
        .AddItem(KelliAttackDelay = new AttackDelayChar())
        .AddItem(SwapDelay = new SwapDelayChar());
    }

    protected override void SetChars()
    {
      Hero.Default = _config.StartHero;
      Health.Default = _config.Health;
      BaseDamage.Default = _config.BaseDamage;
      BaseAttackRatio.Default = _config.BaseAttackRatio;
      KelliAttackRatio.Default = _config.KelliAttackRatio;
      ShonAttackRatio.Default = _config.ShonAttackRatio;
      MoveSpeed.Default = _config.MoveSpeed;
      JumpForce.Default = _config.JumpForce;
      FireDelay.Default = _config.FireDelay;
      FireballSpeed.Default = _config.FireballSpeed;
      DashSpeed.Default = _config.DashSpeed;
      DashDelay.Default = _config.DashDelay;
      KelliAttackDelay.Default = _config.KelliAttackDelay;
      SwapDelay.Default = _config.SwapDelay;
    }
  }
}