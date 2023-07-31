using System;
using SoulRunner.Configuration;
using SoulRunner.Infrastructure;
using Spine.Unity;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerView : View
  {
    public PlayerActionMachine ActiveActionMachine => GetActionMachine(Chars.Hero.Current);
    public Rigidbody2D Rb;
    public GroundChecker GroundChecker;
    public LedgeChecker LedgeChecker;
    
    public Collider2D StayCollider;
    public Collider2D CrouchCollider;

    public PlayerChars Chars;
    public PlayerSpec Spec;
    public PlayerActionVariables ActionVariables = new PlayerActionVariables();

    [Space, Header("Kelli")] 
    public KelliActionMachine KelliMachine;
    public MeshRenderer KelliMesh;
    public KelliAnimator KelliAnim;
    
    public BoneFollower RightHand;
    public BoneFollower LeftHand;
    
    public LayerMask DefaultLayer;
    public LayerMask DashLayer;

    [Space, Header("Shon")] 
    public ShonActionMachine ShonMachine;
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

    public PlayerActionMachine GetActionMachine(HeroType hero) =>
      hero switch
      {
        HeroType.Kelli => KelliMachine,
        HeroType.Shon  => ShonMachine,
        _              => throw new ArgumentOutOfRangeException(nameof(hero), hero, null)
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