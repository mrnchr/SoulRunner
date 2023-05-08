using UnityEngine;
using Spine.Unity;
using Spine;
using System;

namespace SR.Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private static float EMPTY_ANIMATION_DURATION = 0.2f;

        public Action<Transform> onFireAnimStartEvent;
        public Action onFireAnimEndEvent;
        public Action onJumpAttackEndEvent;
        public Action<bool> onRunSwitchAnimEvent;

        [SerializeField]
        private BoneFollower _leftHandSpawn;

        [SerializeField]
        private BoneFollower _rightHandSpawn;

        [SerializeField]
        private SkeletonAnimation _kelliSkeleton;

        [SerializeField]
        private SkeletonAnimation _shonSkeleton;

        private SkeletonAnimation _activeSkeleton;

        private SkeletonAnimation activeSkeleton
        {
            get => _activeSkeleton;
            set {
                _activeSkeleton = value;
                _timeScale = _activeSkeleton.timeScale;
            }
        }


        [SerializeField]
        private KelliAnim _kelliAnim;

        [SerializeField]
        private ShonAnim _shonAnim;

        private bool _isJumping = false;

        public bool isJumping
        {
            get => _isJumping;
            set {
                if (!_isJumping && value) {
                    jumpStart();
                }
                if (_isJumping && !value) {
                    jumpLand();
                }
                _isJumping = value;
            }
        }

        private bool _isCrouching = false;

        public bool isCrouching
        {
            get => _isCrouching;
            set {
                if (!_isCrouching && value) {
                    crouchStart();
                }
                if (_isCrouching && !value) {
                    standUp();
                }
                _isCrouching = value;
            }
        }

        private bool _isDashing = false;

        public bool isDashing
        {
            get => _isDashing;
            set {
                if (!_isDashing && value) {
                    dashStart();
                }
                if (_isDashing && !value) {
                    dashFinish();
                }
                _isDashing = value;
            }
        }

        private bool _isJumpAttack = false;

        public bool isJumpAttack
        {
            get => _isJumping;
            set {
                if (!_isJumpAttack && value) {
                    jumpAttackStart();
                }
                if (_isJumpAttack && !value) {
                    jumpStart();
                }
                _isJumpAttack = value;
            }
        }

        private Vector2 _prevMoveVector = new Vector2();

        private Vector2 _moveVector = new Vector2();

        public Vector2 moveVector
        {
            get => _moveVector;
            set {
                if (_isWalkAnimSwitching) {
                    return;
                }
                if ((_moveVector.x == 0 || !_walkAnim) && value.x != 0 && !_isJumping && !_isCrouching) {
                    _moveVector = value;
                    startMove();
                }
                if (_moveVector.x != 0 && value.x == 0 && !_isJumping && !_isCrouching) {
                    _moveVector = value;
                    stopMove();
                }
                if (_moveVector.x != 0) {
                    _prevMoveVector = _moveVector;
                }
                _moveVector = value;
                if (_moveVector.x != 0 && _tween1 != null) {
                    Debug.Log("cancel tween");
                    LeanTween.cancel(_tween1.id);
                    _tween1 = null;
                    _isWalkAnimSwitching = false;
                }
            }
        }

        private bool _fire1 = false;

        public bool fire1
        {
            get => _fire1;
            set {
                if (value) {
                    if (!_isHanging) {
                        shootLeftHand();
                    } else {
                        shootWall();
                    }
                }
                _fire1 = value;
            }
        }

        private bool _fire2 = false;

        public bool fire2
        {
            get => _fire2;
            set {
                if (value) {
                    if (!_isHanging) {
                        shootRightHand();
                    } else {
                        shootWall();
                    }
                }
                _fire2 = value;
            }
        }

        private float _timeScale;

        public float timeScale
        {
            get => _timeScale;
            private set => _timeScale = value;
            
        }

        private bool _isHanging = false;

        public bool isHanging
        {
            get => _isHanging;
            set {
                if (value) {
                    hangStart();
                } else {
                    hangFinish();
                }
                _isHanging = value;
            }
        }

        private bool _isdead;

        public bool isDead
        {
            get => _isdead;
            set {
                _isdead = value;
                death();
            }
        }

        private bool _walkAnim = false;
        private bool _jumpAnim = false;
        private bool _crouchAnim = false;
        private bool _isWalkAnimSwitching = false;
        private LTDescr _tween1;

        private LTDescr _shootLT;

        private CharacterControl _charController;

        private void Start() {
            _activeSkeleton = _kelliSkeleton;
            _activeSkeleton.state.SetAnimation(0, _kelliAnim.idle, true);
        }

        public void init(CharacterControl charController) {
            _charController = charController;
        }

        public void swapChar() {
            _kelliSkeleton.gameObject.SetActive(_charController.isKelli);
            _shonSkeleton.gameObject.SetActive(!_charController.isKelli);
            _activeSkeleton = _charController.isKelli ? _kelliSkeleton : _shonSkeleton;
            if (_walkAnim) {
                _activeSkeleton.state.SetEmptyAnimation(0, 0);
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.run : _shonAnim.run, true);
            } else if (_jumpAnim) {
                _activeSkeleton.state.SetEmptyAnimation(0, 0);
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.jumpIdle : _shonAnim.jumpIdle, true);
            } else if (_crouchAnim) {
                _activeSkeleton.state.SetEmptyAnimation(0, 0);
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.crouch : _shonAnim.crouch, true);
            } else {
                _activeSkeleton.state.SetEmptyAnimation(0, 0);
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.idle : _shonAnim.idle, true);
            }
        }

        private void startMove() {
            Debug.Log(_moveVector.x + "_" + _prevMoveVector.x);
            if (_moveVector.x == _prevMoveVector.x * -1) {
                //onRunSwitchAnimEvent?.Invoke(true);
                Debug.Log("CHANGE MOVE DIRECTION");
                //_isWalkAnimSwitching = true;
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.run : _shonAnim.run, true);
                //_activeSkeleton.state.SetEmptyAnimation(1, 0);
                //TrackEntry trackEntry = _activeSkeleton.state.AddAnimation(1, _charController.isKelli ? _kelliAnim.runSwitch : _shonAnim.run, false, 0);
                //trackEntry.Complete += switchAnimCompleteHandler;
                
            } else {
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.run : _shonAnim.run, true);
            }  
            _walkAnim = true;
        }

        private void stopMove() {
            Debug.Log("stop move");
            _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.idle : _shonAnim.idle, true);
            _tween1 = LeanTween.delayedCall(0.3f, () => { Debug.Log("sdf"); _tween1 = null; _moveVector.x = 0; onRunSwitchAnimEvent?.Invoke(false); _isWalkAnimSwitching = false; _prevMoveVector.x = 0; });
            _walkAnim = false;
        }

        private void jumpStart() {
            _walkAnim = false;
            _jumpAnim = true;
            _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.jumpStart : _shonAnim.jumpStart, false);
            _activeSkeleton.state.AddAnimation(0, _charController.isKelli ? _kelliAnim.jumpIdle : _shonAnim.jumpIdle, true, 0);
        }

        private void jumpLand() {
            _jumpAnim = false;
            if (!_isJumpAttack) {
                _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.jumpLand : _shonAnim.jumpLanding, false).TimeScale = 2;
                _activeSkeleton.state.AddAnimation(0, _charController.isKelli ? _kelliAnim.idle : _shonAnim.idle, true, 0);
            } else {
                _activeSkeleton.state.SetAnimation(0, _kelliAnim.jumpFireFinish, false).Complete += jumpAttackFinish;
                _activeSkeleton.state.AddAnimation(0, _charController.isKelli ? _kelliAnim.idle : _shonAnim.idle, true, 0);
            }
        }

        private void jumpAttackStart() {
            moveVector = Vector2.zero;
            _activeSkeleton.state.SetAnimation(0, _kelliAnim.jumpFireStart, true);
        }

        private void crouchStart() {
            _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.crouch : _shonAnim.crouch, true);
            _walkAnim = false;
            _crouchAnim = true;
        }

        private void standUp() {
            _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.idle : _shonAnim.idle, true);
            _crouchAnim = false;
        }

        private void dashStart() {
            if (_shootLT != null) {
                _activeSkeleton.state.SetEmptyAnimation(1, 0);
                _shootLT.reset();
                _fire1 = false;
                _fire2 = false;
                _shootLT = null;
            }
            _activeSkeleton.state.SetAnimation(0, _kelliAnim.dash, true);
        }

        private void dashFinish() {
            if (_isJumping) {
                _activeSkeleton.state.SetAnimation(0, _kelliAnim.jumpIdle, true);
            } else if (_moveVector.x != 0) {
                _activeSkeleton.state.SetAnimation(0, _kelliAnim.run, true);
            } else {
                _activeSkeleton.state.SetAnimation(0, _kelliAnim.idle, true);
            }
            
        }

        private void hangStart() {
            _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.wall : _shonAnim.crouch, true);
        }

        private void hangFinish() {
            _activeSkeleton.state.SetAnimation(0, _charController.isKelli ? _kelliAnim.jumpIdle : _shonAnim.crouch, true);
        }

        private void shootLeftHand() {
            Debug.Log("shoot left hand");
            _activeSkeleton.state.SetEmptyAnimation(1, 0.2f);
            TrackEntry track = _activeSkeleton.state.AddAnimation(1, _kelliAnim.fireLeftHand, false, 0);
            _shootLT = LeanTween.delayedCall(0.3f / _activeSkeleton.timeScale, () => { onFireAnimStartEvent?.Invoke(_leftHandSpawn.transform); });
            track.Complete += shootComplete;

        }

        private void shootRightHand() {
            Debug.Log("shoot right hand");
            _activeSkeleton.state.SetEmptyAnimation(1, 0.2f);
            TrackEntry track = _activeSkeleton.state.AddAnimation(1, _kelliAnim.fireRightHand, false, 0);
            _shootLT = LeanTween.delayedCall(0.3f / _activeSkeleton.timeScale, () => { onFireAnimStartEvent?.Invoke(_rightHandSpawn.transform);  });
            track.Complete += shootComplete;
        }

        private void shootWall() {
            Debug.Log("shoot wall");
            _activeSkeleton.state.SetEmptyAnimation(1, 0.2f);
            TrackEntry track = _activeSkeleton.state.AddAnimation(1, _kelliAnim.fireWall, false, 0);
            _shootLT = LeanTween.delayedCall(0.3f / _activeSkeleton.timeScale, () => { onFireAnimStartEvent?.Invoke(_rightHandSpawn.transform); });
            track.Complete += shootComplete;
        }

        private void shootComplete(TrackEntry trackEntry) {
            Debug.Log("shoot complete handler");
            _shootLT = null;
            onFireAnimEndEvent?.Invoke();
            _fire1 = false;
            _fire2 = false;
            trackEntry.Complete -= shootComplete;
            _activeSkeleton.state.SetEmptyAnimation(trackEntry.TrackIndex, 0.2f);
        }

        private void jumpAttackFinish(TrackEntry trackEntry) {
            _isJumpAttack = false;
            onJumpAttackEndEvent?.Invoke();
            trackEntry.Complete -= jumpAttackFinish;
            Debug.Log("jump attack end");
        }

        private void switchAnimCompleteHandler(TrackEntry trackEntry) {
            _isWalkAnimSwitching = false;
            onRunSwitchAnimEvent?.Invoke(false);
            _activeSkeleton.state.SetEmptyAnimation(trackEntry.TrackIndex, 0.2f);
        }

        private void death() {
            _activeSkeleton.state.SetAnimation(0, _kelliAnim.death, false);
        }
    }
}
