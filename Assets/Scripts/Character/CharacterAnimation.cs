// using UnityEngine;
// using Spine.Unity;
// using Spine;
// using System;
// using SoulRunner.Configuration.Anim;
// using UnityEngine.Serialization;
//
// namespace SR.Character.Obsolete
// {
//     public class CharacterAnimation : MonoBehaviour
//     {
//         private static float EMPTY_ANIMATION_DURATION = 0.2f;
//
//         public Action<Transform> onFireAnimStartEvent;
//         public Action onFireAnimEndEvent;
//         public Action onJumpAttackEndEvent;
//         public Action<bool> onRunSwitchAnimEvent;
//
//         [SerializeField]
//         private BoneFollower _leftHandSpawn;
//
//         [SerializeField]
//         private BoneFollower _rightHandSpawn;
//
//         [SerializeField]
//         private SkeletonAnimation _kelliSkeleton;
//
//         [SerializeField]
//         private SkeletonAnimation _shonSkeleton;
//
//         private SkeletonAnimation _activeSkeleton;
//
//         private SkeletonAnimation activeSkeleton
//         {
//             get => _activeSkeleton;
//             set {
//                 _activeSkeleton = value;
//                 _timeScale = _activeSkeleton.timeScale;
//             }
//         }
//
//
//         [FormerlySerializedAs("_kelliAnims"),SerializeField]
//         private KelliAnim _kelliAnim;
//
//         [SerializeField]
//         private ShonAnim _shonAnim;
//
//         private bool _isJumping;
//
//         public bool isJumping
//         {
//             get => _isJumping;
//             set {
//                 if (!_isJumping && value) {
//                     JumpStart();
//                 }
//                 if (_isJumping && !value) {
//                     JumpLand();
//                 }
//                 _isJumping = value;
//             }
//         }
//
//         private bool _isCrouching;
//
//         public bool isCrouching
//         {
//             get => _isCrouching;
//             set {
//                 if (!_isCrouching && value) {
//                     CrouchStart();
//                 }
//                 if (_isCrouching && !value) {
//                     StandUp();
//                 }
//                 _isCrouching = value;
//             }
//         }
//
//         private bool _isDashing;
//
//         public bool isDashing
//         {
//             get => _isDashing;
//             set {
//                 if (!_isDashing && value) {
//                     DashStart();
//                 }
//                 if (_isDashing && !value) {
//                     dashFinish();
//                 }
//                 _isDashing = value;
//             }
//         }
//
//         private bool _isJumpAttack;
//
//         public bool isJumpAttack
//         {
//             get => _isJumping;
//             set {
//                 if (!_isJumpAttack && value) {
//                     JumpAttackStart();
//                 }
//                 if (_isJumpAttack && !value) {
//                     JumpStart();
//                 }
//                 _isJumpAttack = value;
//             }
//         }
//
//         private Vector2 _prevMoveVector;
//
//         private Vector2 _moveVector;
//
//         public Vector2 moveVector
//         {
//             get => _moveVector;
//             set {
//                 if (_isWalkAnimSwitching) {
//                     return;
//                 }
//                 if ((_moveVector.x == 0 || !_walkAnim) && value.x != 0 && !_isJumping && !_isCrouching) {
//                     _moveVector = value;
//                     StartMove();
//                 }
//                 if (_moveVector.x != 0 && value.x == 0 && !_isJumping && !_isCrouching) {
//                     _moveVector = value;
//                     StopMove();
//                 }
//                 if (_moveVector.x != 0) {
//                     _prevMoveVector = _moveVector;
//                 }
//                 _moveVector = value;
//                 if (_moveVector.x != 0 && _tween1 != null) {
//                     LeanTween.cancel(_tween1.id);
//                     _tween1 = null;
//                     _isWalkAnimSwitching = false;
//                 }
//             }
//         }
//
//         private bool _fire1;
//
//         public bool fire1
//         {
//             get => _fire1;
//             set {
//                 if (value) {
//                     if (!_isHanging) {
//                         shootLeftHand();
//                     } else {
//                         shootWall();
//                     }
//                 }
//                 _fire1 = value;
//             }
//         }
//
//         private bool _fire2;
//
//         public bool fire2
//         {
//             get => _fire2;
//             set {
//                 if (value) {
//                     if (!_isHanging) {
//                         shootRightHand();
//                     } else {
//                         shootWall();
//                     }
//                 }
//                 _fire2 = value;
//             }
//         }
//
//         private float _timeScale;
//
//         public float timeScale
//         {
//             get => _timeScale;
//             private set => _timeScale = value;
//             
//         }
//
//         private bool _isHanging;
//
//         public bool isHanging
//         {
//             get => _isHanging;
//             set {
//                 if (value) {
//                     hangStart();
//                 } else {
//                     hangFinish();
//                 }
//                 _isHanging = value;
//             }
//         }
//
//         private bool _isDead;
//
//         public bool isDead
//         {
//             get => _isDead;
//             set {
//                 _isDead = value;
//                 death();
//             }
//         }
//
//         private bool _walkAnim;
//         private bool _jumpAnim;
//         private bool _crouchAnim;
//         private bool _isWalkAnimSwitching;
//         private LTDescr _tween1;
//
//         private LTDescr _shootLT;
//
//         private CharacterControl _charControl;
//
//         private void Start() {
//             _activeSkeleton = _kelliSkeleton;
//             _activeSkeleton.state.SetAnimation(0, _kelliAnim.idle, true);
//         }
//
//         public void Construct(CharacterControl charControl) {
//             _charControl = charControl;
//         }
//
//         public void SwapCharacter() {
//             _kelliSkeleton.gameObject.SetActive(_charControl.isKelli);
//             _shonSkeleton.gameObject.SetActive(!_charControl.isKelli);
//             _activeSkeleton = _charControl.isKelli ? _kelliSkeleton : _shonSkeleton;
//             if (_walkAnim) {
//                 ChangeAnimationByCondition(_charControl.isKelli, _kelliAnim.run, _shonAnim.run);
//             } 
//             else if (_jumpAnim)
//             {
//                 ChangeAnimationByCondition(_charControl.isKelli, _kelliAnim.jumpIdle, _shonAnim.jumpIdle);
//             } 
//             else if (_crouchAnim) 
//             {
//                 ChangeAnimationByCondition(_charControl.isKelli, _kelliAnim.crouch, _shonAnim.crouch);
//             } 
//             else {
//                 ChangeAnimationByCondition(_charControl.isKelli, _kelliAnim.idle, _shonAnim.idle);
//             }
//         }
//
//         private void ChangeAnimationByCondition(bool condition, AnimationReferenceAsset trueCondition,
//                                                 AnimationReferenceAsset falseCondition)
//         {
//             _activeSkeleton.state.SetEmptyAnimation(0, 0);
//             _activeSkeleton.state.SetAnimation(0, condition ? trueCondition : falseCondition, true);
//         }
//
//         private void StartMove() {
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.run : _shonAnim.run, true);
//             _walkAnim = true;
//         }
//
//         private void StopMove() {
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.idle : _shonAnim.idle, true);
//             _tween1 = LeanTween.delayedCall(0.3f, () => { _tween1 = null; _moveVector.x = 0; onRunSwitchAnimEvent?.Invoke(false); _isWalkAnimSwitching = false; _prevMoveVector.x = 0; });
//             _walkAnim = false;
//         }
//
//         private void JumpStart() {
//             _walkAnim = false;
//             _jumpAnim = true;
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.jumpStart : _shonAnim.jumpStart, false);
//             _activeSkeleton.state.AddAnimation(0, _charControl.isKelli ? _kelliAnim.jumpIdle : _shonAnim.jumpIdle, true, 0);
//         }
//
//         private void JumpLand() {
//             _jumpAnim = false;
//             if (!_isJumpAttack) {
//                 _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.jumpLand : _shonAnim.jumpLanding, false).TimeScale = 2;
//                 _activeSkeleton.state.AddAnimation(0, _charControl.isKelli ? _kelliAnim.idle : _shonAnim.idle, true, 0);
//             } else {
//                 _activeSkeleton.state.SetAnimation(0, _kelliAnim.jumpFireFinish, false).Complete += jumpAttackFinish;
//                 _activeSkeleton.state.AddAnimation(0, _charControl.isKelli ? _kelliAnim.idle : _shonAnim.idle, true, 0);
//             }
//         }
//
//         private void JumpAttackStart() {
//             moveVector = Vector2.zero;
//             _activeSkeleton.state.SetAnimation(0, _kelliAnim.jumpFireStart, true);
//         }
//
//         private void CrouchStart() {
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.crouch : _shonAnim.crouch, true);
//             _walkAnim = false;
//             _crouchAnim = true;
//         }
//
//         private void StandUp() {
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.idle : _shonAnim.idle, true);
//             _crouchAnim = false;
//         }
//
//         private void DashStart() {
//             if (_shootLT != null) {
//                 _activeSkeleton.state.SetEmptyAnimation(1, 0);
//                 _shootLT.reset();
//                 _fire1 = false;
//                 _fire2 = false;
//                 _shootLT = null;
//             }
//             _activeSkeleton.state.SetAnimation(0, _kelliAnim.dash, true);
//         }
//
//         private void dashFinish() {
//             if (_isJumping) {
//                 _activeSkeleton.state.SetAnimation(0, _kelliAnim.jumpIdle, true);
//             } else if (_moveVector.x != 0) {
//                 _activeSkeleton.state.SetAnimation(0, _kelliAnim.run, true);
//             } else {
//                 _activeSkeleton.state.SetAnimation(0, _kelliAnim.idle, true);
//             }
//             
//         }
//
//         private void hangStart() {
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.wall : _shonAnim.crouch, true);
//         }
//
//         private void hangFinish() {
//             _activeSkeleton.state.SetAnimation(0, _charControl.isKelli ? _kelliAnim.jumpIdle : _shonAnim.crouch, true);
//         }
//
//         private void shootLeftHand() {
//             _activeSkeleton.state.SetEmptyAnimation(1, 0.2f);
//             TrackEntry track = _activeSkeleton.state.AddAnimation(1, _kelliAnim.fireLeftHand, false, 0);
//             _shootLT = LeanTween.delayedCall(0.3f / _activeSkeleton.timeScale, () => { onFireAnimStartEvent?.Invoke(_leftHandSpawn.transform); });
//             track.Complete += shootComplete;
//
//         }
//
//         private void shootRightHand() {
//             _activeSkeleton.state.SetEmptyAnimation(1, 0.2f);
//             TrackEntry track = _activeSkeleton.state.AddAnimation(1, _kelliAnim.fireRightHand, false, 0);
//             _shootLT = LeanTween.delayedCall(0.3f / _activeSkeleton.timeScale, () => { onFireAnimStartEvent?.Invoke(_rightHandSpawn.transform);  });
//             track.Complete += shootComplete;
//         }
//
//         private void shootWall() {
//             _activeSkeleton.state.SetEmptyAnimation(1, 0.2f);
//             TrackEntry track = _activeSkeleton.state.AddAnimation(1, _kelliAnim.fireWall, false, 0);
//             _shootLT = LeanTween.delayedCall(0.3f / _activeSkeleton.timeScale, () => { onFireAnimStartEvent?.Invoke(_rightHandSpawn.transform); });
//             track.Complete += shootComplete;
//         }
//
//         private void shootComplete(TrackEntry trackEntry) {
//             _shootLT = null;
//             onFireAnimEndEvent?.Invoke();
//             _fire1 = false;
//             _fire2 = false;
//             trackEntry.Complete -= shootComplete;
//             _activeSkeleton.state.SetEmptyAnimation(trackEntry.TrackIndex, 0.2f);
//         }
//
//         private void jumpAttackFinish(TrackEntry trackEntry) {
//             _isJumpAttack = false;
//             onJumpAttackEndEvent?.Invoke();
//             trackEntry.Complete -= jumpAttackFinish;
//         }
//
//         private void switchAnimCompleteHandler(TrackEntry trackEntry) {
//             _isWalkAnimSwitching = false;
//             onRunSwitchAnimEvent?.Invoke(false);
//             _activeSkeleton.state.SetEmptyAnimation(trackEntry.TrackIndex, 0.2f);
//         }
//
//         private void death() {
//             _activeSkeleton.state.SetAnimation(0, _kelliAnim.death, false);
//         }
//     }
// }
