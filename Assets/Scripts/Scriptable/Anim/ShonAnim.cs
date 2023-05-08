using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anim", menuName = "ScriptableObjects/ShonAnim", order = 1)]
public class ShonAnim : ScriptableObject
{
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset crouch;
    public AnimationReferenceAsset attackStand;
    public AnimationReferenceAsset attackCrouch;
    public AnimationReferenceAsset jumpStart;
    public AnimationReferenceAsset jumpIdle;
    public AnimationReferenceAsset jumpAirJump;
    public AnimationReferenceAsset jumpLanding;
    public AnimationReferenceAsset jumpLand;


}
