using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineStateBehaviour : StateMachineBehaviour
{
  public AnimationReferenceAsset AnimationAsset;
  public bool IsLoop;

  private SkeletonAnimation _anim;
  private bool _isInitialized;

  private void Initialize(Animator animator)
  {
    if (_isInitialized) return;
    
    _anim = animator.GetComponent<SkeletonAnimation>();
    _isInitialized = true;
  }

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    Initialize(animator);
    _anim.state.SetAnimation(0, AnimationAsset, IsLoop);
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    
  //}

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    
  //}

  // OnStateMove is called right after Animator.OnAnimatorMove()
  //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that processes and affects root motion
  //}

  // OnStateIK is called right after Animator.OnAnimatorIK()
  //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that sets up animation IK (inverse kinematics)
  //}
}