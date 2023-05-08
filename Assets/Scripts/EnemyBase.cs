using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [SerializeField]
    private SkeletonAnimation _skeleton;

    [SerializeField]
    private AnimationReferenceAsset _attackAnim;

    [SerializeField]
    private BoxCollider2D _collider;

    private TrackEntry _trackEntry;

    // Start is called before the first frame update
    void Start()
    {
        _trackEntry = _skeleton.state.SetAnimation(0, _attackAnim, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_trackEntry.AnimationTime > 0.5f && _trackEntry.AnimationTime < 0.6f) {
            _collider.enabled = true;
        } else {
            _collider.enabled = false;
        }
        //Debug.Log(_trackEntry.AnimationTime);
    }
}
