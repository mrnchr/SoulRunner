using System;
using SoulRunner.Configuration;
using Spine.Unity;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerView : View
  {
    public Rigidbody2D Rb;
    public PlayerActionMachine ActionMachine;
    public GroundChecker GroundChecker;
    public LedgeChecker LedgeChecker;
    
    public Collider2D StayCollider;
    public Collider2D CrouchCollider;
    
    public PlayerConfig PlayerCfg;
    public PlayerActionVariables ActionVariables = new PlayerActionVariables();

    [Space, Header("Kelli")] 
    public MeshRenderer KelliMesh;
    public KelliAnimator KelliAnim;
    
    public BoneFollower RightHand;
    public BoneFollower LeftHand;
    
    public LayerMask DefaultLayer;
    public LayerMask DashLayer;

    [Space, Header("Shon")] 
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

    [Inject]
    public void Construct(PlayerConfig playerCfg)
    {
      PlayerCfg = playerCfg;
    }
    
    private void Reset()
    {
      TryGetComponent(out Rb);
    }
  }
}