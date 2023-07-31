using System.Collections.Generic;
using System.Linq;
using SoulRunner.Configuration;
using UnityEngine;

namespace SoulRunner.Characteristics
{
  public abstract class CharacteristicList<TConfig> : MonoBehaviour, ICharacteristicList
  where TConfig : IConfig
  {
    protected TConfig _config;
    protected readonly List<ICharacteristic> _chars = new List<ICharacteristic>();

    public virtual CharMask<TChar> GetChars<TChar>()
      where TChar : ICharacteristic =>
      new CharMask<TChar>(_chars.OfType<TChar>());

    protected void SetConfig(TConfig config)
    {
      _config = config;
      SetChars();
      ResetChars();
    }
    
    public void ResetChars()
    {
      foreach (IDefaultChar defaultChar in _chars.OfType<IDefaultChar>())
        defaultChar.ToDefault();
    }

    protected abstract void FillChars();
    protected abstract void SetChars();
  }
}