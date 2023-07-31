using System;
using System.Linq;
using SoulRunner.Characteristics;
using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using SoulRunner.Utility;
using Zenject;

namespace SoulRunner.Player
{
  [Serializable]
  public sealed class PlayerChars : CharacteristicList<PlayerConfig>
  {
    public HeroChar Hero;
    public MoveSpeedChar MoveSpeed;
    public JumpForceChar JumpForce;
    public FireDelayChar FireDelay;
    public DashSpeedChar DashSpeed;
    public DashDelayChar DashDelay;
    public SwapDelayChar SwapDelay;

    [Inject]
    public void Construct(IConfigService configSvc)
    {
      FillChars();
      SetConfig(configSvc.GetConfig<PlayerConfig>());
    }
    
    public override CharMask<TChar> GetChars<TChar>() => new PlayerCharMask<TChar>(_chars.OfType<TChar>());

    protected override void FillChars()
    {
      _chars
        .AddItem(Hero = new HeroChar())
        .AddItem(MoveSpeed = new MoveSpeedChar())
        .AddItem(JumpForce = new JumpForceChar())
        .AddItem(FireDelay = new FireDelayChar())
        .AddItem(DashSpeed = new DashSpeedChar())
        .AddItem(DashDelay = new DashDelayChar())
        .AddItem(SwapDelay = new SwapDelayChar());
    }

    protected override void SetChars()
    {
      Hero.Default = _config.StartHero;
      MoveSpeed.Default = _config.MoveSpeed;
      JumpForce.Default = _config.JumpForce;
      FireDelay.Default = _config.FireDelay;
      DashSpeed.Default = _config.DashSpeed;
      DashDelay.Default = _config.DashDelay;
      SwapDelay.Default = _config.SwapDelay;
    }
  }
}