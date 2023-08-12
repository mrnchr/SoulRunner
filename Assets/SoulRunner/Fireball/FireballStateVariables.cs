using System;
using SoulRunner.Infrastructure;

namespace SoulRunner.Fireball
{
  [Serializable]
  public class FireballStateVariables
  {
    public Action OnDeathStart;
    
    public bool IsDying;
    public bool IsCollided;
  }
}