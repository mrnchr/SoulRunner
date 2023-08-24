namespace SoulRunner.Player
{
  public static class PlayerCharsExtensions
  {
    public static float GetEnergyCost(this PlayerChars obj) =>
      obj.BaseEnergyRatio * obj.GetChars<HeroEnergyCostChar>().Char;
  }
}