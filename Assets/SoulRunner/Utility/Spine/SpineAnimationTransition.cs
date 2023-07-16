using System;

namespace SoulRunner.Utility.Spine
{
  public class SpineAnimationTransition<TEnum>
    where TEnum : Enum
  {
    public SpineAnimationState<TEnum> Destination;
    public bool IsHold;
    public Func<bool> Condition;
  }
}