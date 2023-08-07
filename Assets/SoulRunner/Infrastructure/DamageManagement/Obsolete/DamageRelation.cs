using System;
using System.Collections.Generic;

namespace SoulRunner.Infrastructure
{
  [Obsolete]
  public class DamageRelation
  {
    public List<ObjectType> Damaged = new List<ObjectType>();
    public List<ObjectType> Damaging = new List<ObjectType>();
    public bool IsInTurn;
  }
}