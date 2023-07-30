using System;

namespace SoulRunner.Infrastructure.Spine
{
  public class SpineAnimationTransition<TEnum>
    where TEnum : Enum
  {
    public SpineAnimationState<TEnum> Destination;
    public bool IsHold;
    public Func<bool> Condition;
  }
}