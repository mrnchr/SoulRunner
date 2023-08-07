using System;
using SoulRunner.Infrastructure;

namespace SoulRunner.Fireball
{
  [Serializable]
  public class FireballActionVariables
  {
    public Action OnDeathStart;
    
    public bool IsDying;
  }
}