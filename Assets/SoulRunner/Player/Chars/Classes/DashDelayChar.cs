using System;
using SoulRunner.Characteristics;
using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  [Serializable]
  public class DashDelayChar : Characteristic<Timer>, IKelliChar
  {
  }
}