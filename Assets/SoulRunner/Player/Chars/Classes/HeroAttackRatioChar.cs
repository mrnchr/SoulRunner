﻿using System;
using SoulRunner.Infrastructure;

namespace SoulRunner.Player
{
  [Serializable]
  public class HeroAttackRatioChar : AttackRatioChar, IHeroChar
  {
    public ObjectType Owner { get; set; }
  }
}