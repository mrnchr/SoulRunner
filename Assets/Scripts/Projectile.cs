using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class Projectile : MonoBehaviour
{

    public static Action<Projectile> onDisposeEvent;


    [SerializeField]
    private AnimationReferenceAsset _spawn;

    [SerializeField]
    private AnimationReferenceAsset _idle;

    [SerializeField]
    private AnimationReferenceAsset _despawn;

    private SkeletonAnimation _skeleton;
    private Rigidbody2D _rb;

    private Vector2 _moveVector = new Vector2();
    private float _lifeTime = 5;
    private bool _isLive = false;
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _skeleton = GetComponent<SkeletonAnimation>();
    }

    public void init(Vector2 direction) {

        transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);

        _moveVector.x = direction.x * 2;
        _moveVector.y = direction.y;

        _isLive = true;

        _skeleton.state.SetAnimation(0, _spawn, false);
        _skeleton.state.AddAnimation(0, _idle, true, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        unspawn();
    }

    private void dispose() {
        onDisposeEvent?.Invoke(this);
        Destroy(gameObject);
    }
    
    private void unspawn() {
        _moveVector.x = _moveVector.y = 0;
        TrackEntry despawnTrack = _skeleton.state.SetAnimation(0, _despawn, false);
        despawnTrack.Complete += despawnCompleteHandler;
    }

    public void step(float dt) {
        _rb.velocity = _moveVector;
        if (_isLive) {
            if (_lifeTime > 0) {
                _lifeTime -= dt;
            } else {
                _lifeTime = 0;
                _isLive = false;
                unspawn();
            }
        }
    }

    private void despawnCompleteHandler(TrackEntry trackEntry) {
        trackEntry.Complete -= despawnCompleteHandler;
        dispose();
    }
}
