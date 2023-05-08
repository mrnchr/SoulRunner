using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anim", menuName = "ScriptableObjects/KelliAnim", order = 1)]
public class KelliAnim : ScriptableObject
{
    
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset crouch;
    public AnimationReferenceAsset dash;
    public AnimationReferenceAsset death;
    public AnimationReferenceAsset wall;
    public AnimationReferenceAsset runSwitch;
    public AnimationReferenceAsset jumpStart;
    public AnimationReferenceAsset jumpIdle;
    public AnimationReferenceAsset jumpLand;
    public AnimationReferenceAsset fireLeftHand;
    public AnimationReferenceAsset fireRightHand;
    public AnimationReferenceAsset fireWall;
    public AnimationReferenceAsset jumpFireStart;
    public AnimationReferenceAsset jumpFireFinish;


}
