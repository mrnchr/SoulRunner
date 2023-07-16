using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;

namespace SoulRunner.Utility.Spine
{
  public class SpineAnimatorLayer<TEnum>
  where TEnum : Enum
  {
    public int Id;
    public SpineAnimationState<TEnum> Start;
    public SpineAnimationState<TEnum> Current;
    public SpineAnimationState<TEnum> Next;
    public List<SpineAnimationState<TEnum>> States = new List<SpineAnimationState<TEnum>>();
    public SkeletonAnimation Skeleton;

    public SpineAnimatorLayer(int id, SkeletonAnimation skeleton)
    {
      Id = id;
      Skeleton = skeleton;
    }
    
    public void ChangeAnimation(SpineAnimationState<TEnum> to)
    {
      Current = to;
      if (Current.Animation.Asset)
        Skeleton.state.SetAnimation(Id, Current.Animation.Asset, Current.Animation.IsLoop);
      else
        Skeleton.state.SetEmptyAnimation(Id, 0);
      CheckTransition();
    }

    public void CheckTransition()
    {
      ClearNext();
      var transition = Current.FindFirstCompletedCondition();
      if (transition != null)
      {
        if (transition.IsHold)
        {
          // Debug.Log($"Transition hold {transition.Destination.Animation.Name}");
          DelayAnimation(transition.Destination);
        }
        else
        {
          // Debug.Log($"Transition change {transition.Destination.Animation.Name}");
          ChangeAnimation(transition.Destination);
        }
      }
    }

    public void ClearNext()
    {
      Next = null;
      Skeleton.state.Complete -= OnAnimationCompleted;
    }

    public void DelayAnimation(SpineAnimationState<TEnum> to)
    {
      // Debug.Log(_skeleton.state.Tracks.Items[0].Animation.Name);
      // Debug.Log($"Delay {to.Animation.Name}");
      Next = to;
      Skeleton.state.Complete += OnAnimationCompleted;
    }

    public void OnAnimationCompleted(TrackEntry trackentry)
    {
      if (trackentry.Animation != Current.Animation.Asset.Animation && trackentry.TrackIndex != Id) return;
      
      // Debug.Log(trackentry.Animation.Name);
      // Debug.Log($"Transition {_next.Animation.Name}");
      ChangeAnimation(Next);
      Skeleton.state.Complete -= OnAnimationCompleted;
    }
  }
}