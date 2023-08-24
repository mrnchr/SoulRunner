using SoulRunner.Characteristics;
using SoulRunner.Configuration;
using SoulRunner.Player;
using SoulRunner.Utility;

namespace SoulRunner.Enemies
{
  public class GriblenChars : CharacteristicList<GriblenConfig>
  {
    public HealthChar Health;
    
    protected override void FillChars()
    {
      _chars
        .AddItem(Health = new HealthChar());
    }

    protected override void SetChars()
    {
      Health.Default = _config.Health;
    }
  }
}