namespace SoulRunner.Characteristics
{
  public interface ICharacteristicList
  {
    public CharMask<TChar> GetChars<TChar>()
      where TChar : ICharacteristic;

    public void ResetChars();
  }
}