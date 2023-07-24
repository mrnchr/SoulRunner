using System;
using System.Collections;
using SoulRunner.Player.Movement;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerView : View
  {
    public Rigidbody2D Rb;
    // public ActionMachine CurrentMachine;
    public KelliAnimator CurrentAnimator;
    public GroundChecker GroundChecker;
    public LedgeChecker LedgeChecker;
    
    public Collider2D StayCollider;
    public Collider2D CrouchCollider;
    
    public BoneFollower RightHand;
    public BoneFollower LeftHand;
    
    public LayerMask DefaultLayer;
    public LayerMask DashLayer;

    [FormerlySerializedAs("HeroCfg"),HideInInspector] public PlayerConfig PlayerCfg;

    public BoneFollower GetHand(HandType hand) =>
      hand switch
      {
        HandType.Left  => LeftHand,
        HandType.Right => RightHand,
        HandType.None  => throw new ArgumentOutOfRangeException(nameof(hand), hand, null),
        _              => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
      };

    [Inject]
    public void Construct(PlayerConfig playerCfg)
    {
      PlayerCfg = playerCfg;
    }
    
    private void Awake()
    {
      GroundChecker.View = this;
      LedgeChecker.View = this;
    }

    private void Reset()
    {
      TryGetComponent(out Rb);
      TryGetComponent(out CurrentAnimator);
    }
  }
}