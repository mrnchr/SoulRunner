using System.Collections.Generic;
using SoulRunner.Infrastructure;
using UnityEngine;
using Zenject;

namespace SoulRunner.Characteristics
{
  public abstract class CharacteristicList<TConfig> : MonoBehaviour, ICharacteristicList
  where TConfig : IConfig
  {
    protected TConfig _config;
    protected readonly List<ICharacteristic> _chars = new List<ICharacteristic>();

    [Inject]
    public void Construct(IConfigurationService configSvc)
    {
      FillChars();
      SetConfig(configSvc.GetConfig<TConfig>());
    }

    public virtual List<ICharacteristic> GetChars() => _chars;
    public virtual CharMask<TChar> GetChars<TChar>()
      where TChar : ICharacteristic =>
      new CharMask<TChar>(_chars);

    public void ResetChars()
    {
      foreach (ICharacteristic defaultChar in _chars)
        defaultChar.Init();
    }

    protected void SetConfig(TConfig config)
    {
      _config = config;
      SetChars();
      ResetChars();
    }

    protected abstract void FillChars();
    protected abstract void SetChars();
  }
}