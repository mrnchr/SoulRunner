using System;
using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerView : View
  {
    public PlayerStateMachine ActiveMachine => GetMachine(Chars.Hero.Current);
    public Rigidbody2D Rb;
    public GroundChecker GroundChecker;
    public LedgeChecker LedgeChecker;

    public Collider2D StayCollider;
    public Collider2D CrouchCollider;
    public Collider2D AttackCollider;
    public Collider2D KelliAttackTrigger;

    public new PlayerChars Chars;
    [HideInInspector] public PlayerSpec Spec;
    public PlayerStateVariables StateVariables = new PlayerStateVariables();

    [Space, Header("Kelli")] 
    public KelliStateMachine KelliMachine;
    public MeshRenderer KelliMesh;
    public KelliAnimator KelliAnim;
    
    public BoneFollower RightHand;
    public BoneFollower LeftHand;
    
    public LayerMask DefaultLayer;
    public LayerMask DashLayer;

    [Space, Header("Shon")] 
    public ShonStateMachine ShonMachine;
    public MeshRenderer ShonMesh;
    public ShonAnimator ShonAnim;

    public BoneFollower GetHand(HandType hand) =>
      hand switch
      {
        HandType.Left  => LeftHand,
        HandType.Right => RightHand,
        HandType.None  => throw new ArgumentOutOfRangeException(nameof(hand), hand, null),
        _              => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
      };

    public PlayerStateMachine GetMachine(ObjectType hero) =>
      hero switch
      {
        ObjectType.Kelli => KelliMachine,
        ObjectType.Shon  => ShonMachine,
        _                => throw new ArgumentOutOfRangeException(nameof(hero), hero, null)
      };

    [Inject]
    public void Construct(ISpecificationService specSvc)
    {
      Spec = specSvc.GetSpec<PlayerSpec>();
    }

    private void Reset()
    {
      TryGetComponent(out Rb);
    }
  }
}