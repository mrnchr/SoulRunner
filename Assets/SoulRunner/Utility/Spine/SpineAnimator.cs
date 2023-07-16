using System;
using System.Linq;
using Spine;
using Spine.Unity;

namespace SoulRunner.Utility.Spine
{
  public class SpineAnimator<TEnum>
    where TEnum : Enum
  {
    public SpineAnimationState<TEnum> Current { get; private set; }

    private readonly SpineAnimationState<TEnum> _start;
    private readonly SpineAnimationState<TEnum>[] _states;
    private readonly SkeletonAnimation _skeleton;
    private SpineAnimationState<TEnum> _next;

    public SpineAnimator(SkeletonAnimation skeleton, ConfigurableSpineAnimation<TEnum>[] anims, TEnum start)
    {
      _skeleton = skeleton;
      _states = new SpineAnimationState<TEnum>[anims.Length];
      for (int i = 0; i < _states.Length; i++)
      {
        _states[i] = new SpineAnimationState<TEnum>(anims[i]);
      }

      _start = GetState(start);
    }

    public SpineAnimator<TEnum> AddTransition(TEnum from, TEnum to, Func<bool> condition, bool isHold = false)
    {
      GetState(from)
        .Transitions
        .Add(
          new SpineAnimationTransition<TEnum>
          {
            Destination = GetState(to),
            IsHold = isHold,
            Condition = condition
          });

      return this;
    }

    public SpineAnimationState<TEnum> GetState(TEnum type) =>
      _states.First(x => x.Animation.Name.Equals(type));

    public void SetVariable<T>(T value, ref T variable)
    {
      bool needCheck = !value.Equals(variable);
      variable = value;

      if (needCheck)
        CheckTransition();
    }

    public void StartAnimate() => ChangeAnimation(_start);

    private void ChangeAnimation(SpineAnimationState<TEnum> to)
    {
      // Debug.Log($"Change {to.Animation.Name}");
      Current = to;
      _skeleton.state.SetAnimation(0, Current.Animation.Asset, Current.Animation.IsLoop);
      CheckTransition();
    }

    private void CheckTransition()
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

    private void ClearNext()
    {
      _next = null;
      _skeleton.state.Complete -= OnAnimationCompleted;
    }

    private void DelayAnimation(SpineAnimationState<TEnum> to)
    {
      // Debug.Log(_skeleton.state.Tracks.Items[0].Animation.Name);
      // Debug.Log($"Delay {to.Animation.Name}");
      _next = to;
      _skeleton.state.Complete += OnAnimationCompleted;
    }

    private void OnAnimationCompleted(TrackEntry trackentry)
    {
      if (trackentry.Animation != Current.Animation.Asset.Animation) return;
      
      // Debug.Log(trackentry.Animation.Name);
      // Debug.Log($"Transition {_next.Animation.Name}");
      ChangeAnimation(_next);
      _skeleton.state.Complete -= OnAnimationCompleted;
    }
  }
}