using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerView : View
  {
    public Rigidbody2D Rb;
    public KelliAnimator CurrentAnimator;
    public GroundChecker GroundChecker;
    
    public Collider2D StayCollider;
    public Collider2D CrouchCollider;
    
    public BoneFollower RightHand;
    public BoneFollower LeftHand;
    
    public BoneFollower GetHand(HandType hand) =>
      hand switch
      {
        HandType.Left  => LeftHand,
        HandType.Right => RightHand,
        _              => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
      };
    
    private void Awake()
    {
      StartCoroutine(WaitEcs());
    }

    private IEnumerator WaitEcs()
    {
      yield return new WaitUntil(() => IsEcsActive);
      GroundChecker.Entity = Entity;
    }

    private void Reset()
    {
      TryGetComponent(out Rb);
      TryGetComponent(out CurrentAnimator);
    }
  }
}