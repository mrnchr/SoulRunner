using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;

namespace SR.Character
{
    public class CharacterControl : MonoBehaviour
    {

        private const float SHOOT_DELAY = 0.2f;

        public static Action<float> onAbility1CDChange;
        public static Action<float> onAbility2CDChange;
        public static Action<float> onShootCDChange;

        private bool _isKelli = true;

        public bool isKelli
        {
            get => _isKelli;
            set {
                _isKelli = value;
                _anim.swapChar();
            }
        }

        [SerializeField]
        private float _jumpStrength;

        [SerializeField]
        private float _moveSpeed;

        [SerializeField]
        private GameObject _projectile;

        [SerializeField]
        private GroundChecker _groundChecker;

        [SerializeField]
        private LedgeChecker _ledgeChecker;

        private Player _player;

        private Player player {
            get {
                _player = _player == null ? (ReInput.isReady ? ReInput.players.GetPlayer(0) : null) : _player;
                return _player;
            }
        }

        private Vector3 _moveVector = new Vector3();

        private bool _isJumping = false;

        private bool isJumping
        {
            get => _isJumping;
            set {
                _isJumping = value;
                _anim.isJumping = _isJumping;
                if (_isJumping) {
                    _ledgeChecker.gameObject.SetActive(false);
                    LeanTween.delayedCall(0.2f, () => { _ledgeChecker.gameObject.SetActive(true); });
                } else {
                    _ledgeChecker.gameObject.SetActive(false);
                }
            }
        }

        private bool _isCrouching = false;

        private bool isCrouching
        {
            get => _isCrouching;
            set {
                _isCrouching = value;
                _anim.isCrouching = _isCrouching;
            }
        }

        private bool _isHanging = false;

        private bool isHanging
        {
            get => _isHanging;
            set {
                if (value) {
                    if (_isDashing) {
                        _isDashing = false;
                    }
                    _moveVector = Vector3.zero;
                    _rb.bodyType = RigidbodyType2D.Kinematic;
                    _rb.velocity = Vector3.zero;
                    _isJumping = false;
                    _anim.isHanging = true;
                    //_ledgeChecker.gameObject.SetActive(false);
                } else {
                    _rb.bodyType = RigidbodyType2D.Dynamic;
                    _anim.isHanging = false;
                    _isJumping = true;
                    _anim.isJumping = _isJumping;
                }
                _isHanging = value;
            }
        }

        private CharacterAnimation _anim;
        private Rigidbody2D _rb;

        private Vector2 _jumpVec = new Vector2();
        private bool _lastFireHandRight = true;
        private List<Projectile> _projectiles = new List<Projectile>();

        private float _fireDelay = 0;

        private float fireDelay
        {
            get => _fireDelay;
            set {
                _fireDelay = value;
                onShootCDChange?.Invoke(_fireDelay / 0.2f);
            }
        }

        private float _ability1Delay = 0;

        private float ability1Delay
        {
            get => _ability1Delay;
            set {
                _ability1Delay = value;
                onAbility1CDChange?.Invoke(_ability1Delay);
            }
        }

        private float _ability2Delay = 0;

        private float ability2Delay
        {
            get => _ability2Delay;
            set {
                _ability2Delay = value;
                onAbility2CDChange?.Invoke(_ability2Delay / 5);
            }
        }
        private bool _isDashing = false;
        private bool _isJumpAttacking = false;
        private bool _isSwitchingMoveDir;
        private float _dashDuration = 0;
        private Vector2 _tempVel;
        private bool _isDead = false;
        private Vector2 _startPos;

        public void respawn() {
            _rb.velocity = Vector2.zero;
            _moveVector = Vector2.zero;
            transform.position = _startPos;
            if (_isJumpAttacking) {
                _anim.isJumpAttack = false;
                _isJumpAttacking = false;
            }
        }

        private void Awake() {
            _anim = GetComponent<CharacterAnimation>();
            _anim.init(this);
            _rb = GetComponent<Rigidbody2D>();
            _jumpVec.y = _jumpStrength * 100;
            _startPos = transform.position;
        }

        private void OnEnable() {
            _anim.onFireAnimStartEvent += fireAnimStartHandler;
            _anim.onFireAnimEndEvent += fireAnimEndHandler;
            _anim.onRunSwitchAnimEvent += runSwitchAnimHandler;
            _anim.onJumpAttackEndEvent += jumpAttackEndHandler;
            Projectile.onDisposeEvent += projectileDisposeHandler;
            Level.onPauseStateChangedEvent += pauseStateChangedHandler;
            User.onDeathEvent += deathEventHandler;
            _groundChecker.onTriggerEnter += groundTriggerEnterHandler;
            _groundChecker.onTriggerExit += groundTriggerExitHandler;
            _ledgeChecker.onTriggerEnter += ledgeTriggerEnterHandler;
            _ledgeChecker.onTriggerExit += ledgeTriggerExitHandler;
        }

        private void OnDisable() {
            _anim.onFireAnimStartEvent -= fireAnimStartHandler;
            _anim.onFireAnimEndEvent -= fireAnimEndHandler;
            _anim.onRunSwitchAnimEvent -= runSwitchAnimHandler;
            _anim.onJumpAttackEndEvent -= jumpAttackEndHandler;
            Projectile.onDisposeEvent -= projectileDisposeHandler;
            Level.onPauseStateChangedEvent -= pauseStateChangedHandler;
            User.onDeathEvent -= deathEventHandler;
            _groundChecker.onTriggerEnter -= groundTriggerEnterHandler;
            _groundChecker.onTriggerEnter -= groundTriggerExitHandler;
            _ledgeChecker.onTriggerEnter -= ledgeTriggerEnterHandler;
            _ledgeChecker.onTriggerExit -= ledgeTriggerExitHandler;
        }

        private void ProcessInput() {

           
            if (_isJumpAttacking) {
                return;
            }
            if (!_isDashing) {
                if (!_isCrouching && !_isHanging) {
                    _moveVector.x = player.GetAxis("Horizontal") * _moveSpeed;
                }
                /*if (_isSwitchingMoveDir) {
                    _moveVector.x = 0;
                }*/
                _moveVector.y = _rb.velocity.y;
                _anim.moveVector = _moveVector;
                if (_moveVector.x < 0 && transform.localScale.x != -1) {
                    transform.localScale = new Vector3(-1, 1, 1);
                } else if (_moveVector.x > 0 && transform.localScale.x != 1) {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                
                if (player.GetButtonDown("Jump") && !_isJumping && !_isCrouching) {
                    if (_isHanging) {
                        isHanging = false;
                    }
                    isJumping = true;
                    _rb.velocity = new Vector2(_rb.velocity.x, 0);
                    _moveVector.y = 0;
                    _rb.AddForce(_jumpVec);
                } else if (player.GetButton("Crouch") && !_isCrouching && !_isJumping) {
                    if (_isHanging) {
                        isHanging = false;
                    } else {
                        isCrouching = true;
                        _moveVector.x = 0;
                    }
                }
                if (_isCrouching && player.GetButtonUp("Crouch")) {
                    isCrouching = false;
                }
                if (_fireDelay <= 0 ) {
                    if (player.GetButtonDown("Fire1")/* && _lastFireHandRight*/ && User.mp(0) >= 10) {
                        _anim.fire1 = true;
                        fireDelay = SHOOT_DELAY;
                        _lastFireHandRight = false;
                    }
                    if (player.GetButtonDown("Fire2") /*&& !_lastFireHandRight*/&& User.mp(0) >= 10) {
                        _anim.fire2 = true;
                        fireDelay = SHOOT_DELAY;
                        _lastFireHandRight = true;
                    }
                }
            }
            if (player.GetButtonDown("Ability1") && _ability1Delay <= 0 && _isKelli && !_isCrouching && !_isHanging) {
                _isDashing = true;
                ability1Delay = 1;
                _moveVector.x = 20 * transform.localScale.x;
                _dashDuration = 0.2f;
                _anim.isDashing = true;
                gameObject.layer = 10;
            }
            if (player.GetButtonDown("Ability2") && _ability2Delay <= 0 && _isKelli && _isJumping && !_isHanging) {
                _moveVector.x = 0;
                _moveVector.y = Mathf.Min(-10, _moveVector.y);
                _isJumpAttacking = true;
                ability2Delay = 5;
                _anim.isJumpAttack = true;
            }
            if (player.GetButtonDown("SwapChar") && !_isDashing) {
                isKelli = !isKelli;
            }
            
            if (player.GetButtonDown("NextItem")) {
                User.inventory.nextUseSlot();
            } else if (player.GetButtonDown("PrevItem")) {
                User.inventory.prevUseSlot();
            }
            if (player.GetButtonDown("Use")) {
                User.inventory.useSelectedItem();
            }

        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (_isDead) {
                return;
            }
            if (collision.CompareTag("Collectable")) {
                collision.gameObject.GetComponent<Collectable>().collect();
            } else if (collision.CompareTag("Enemy")) {
                User.damage(10);
            }
        }

        private void LateUpdate() {
            if (Level.isPaused || _isDead) {
                return;
            }
            ProcessInput();
            //if (_moveVector.x != 0 && !_isCrouching) {
            if (!_isHanging) {
                _rb.velocity = _moveVector;
            }
            /*} else {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            */
            float dt = Time.deltaTime;
            if (_ability1Delay > 0) {
                ability1Delay -= dt;
            }
            if (_ability2Delay > 0) {
                ability2Delay -= dt;
            }
            if (_fireDelay > 0) {
                fireDelay -= dt;
            }
            if (_isDashing) {
                _moveVector.y = 0;
                if (_dashDuration > 0) {
                    _dashDuration -= Time.deltaTime;
                } else {
                    _isDashing = false;
                    _anim.isDashing = false;
                    gameObject.layer = 6;
                }
            }
            if (_isJumpAttacking) {
                _moveVector.y -= 20 * dt;
            }
            for (int i = 0; i < _projectiles.Count; i++) {
                _projectiles[i].step(dt);
            }
        }

        private void fireAnimStartHandler(Transform container) {
            User.spendMana(10);
            GameObject projectileGO = Instantiate(_projectile);
            projectileGO.transform.position = container.transform.position - new Vector3(0.2f, 1, 0);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.init(new Vector2(10 * transform.localScale.x * (_isHanging ? -1 : 1), 0));
            _projectiles.Add(projectile);
        }

        private void fireAnimEndHandler() {
        }

        private void runSwitchAnimHandler(bool isActivated) {
            if (isActivated) {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
            }
            _isSwitchingMoveDir = isActivated;
        }

        private void jumpAttackEndHandler() {
            _isJumpAttacking = false;
        }

        private void projectileDisposeHandler(Projectile projectile) {
            for (int i = 0; i < _projectiles.Count; i++) {
                if (_projectiles[i] == projectile) {
                    _projectiles.RemoveAt(i);
                    return;
                }
            }

        }

        private void groundTriggerEnterHandler() {
            isJumping = false;
        }

        private void groundTriggerExitHandler() {
            if (!_isJumping || !_isJumpAttacking) {
                isJumping = true;
            }
        }

        private void ledgeTriggerEnterHandler(Transform colliderTransform) {
            
            isHanging = true;
            transform.position = new Vector3(colliderTransform.position.x - _ledgeChecker.transform.localPosition.x * (transform.localScale.x), transform.position.y);
        }

        private void ledgeTriggerExitHandler() {
            Debug.Log("ledge trigger exit");
        }

        private void pauseStateChangedHandler(bool val) {
            if (val) {
                _tempVel = _rb.velocity;
                _rb.isKinematic = true;
                _rb.velocity = Vector2.zero;
            } else {
                _rb.isKinematic = false;
                _rb.velocity = _tempVel;
            }
        }

        private void deathEventHandler() {
            _isDead = true;
            _anim.isDead = true;
        }
    }
}
