using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Player;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs;
using UnityEngine;

namespace SoulRunner.Control
{
  public class InputSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private readonly EcsCustomInject<InputReader> _inputReader = default;
    private InputReader _input;
    private int _player;
    private EcsWorld _world;

    private float _moveDirection;
    private bool _isJump;
    private bool _isCrouch;
    private HandType _nextHand;
    private bool _isDash;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
      _input = _inputReader.Value;
      _input.OnMove += x => _moveDirection = x;
      _input.OnJump += () => _isJump = true;
      _input.OnCrouch += () => _isCrouch = true;
      _input.OnFireLeft += () => _nextHand = HandType.Left;
      _input.OnFireRight += () => _nextHand = HandType.Right;
      _input.OnDash += () => _isDash = true;
    }

    public void Run(IEcsSystems systems)
    {
      _player = _world
        .Filter<ControllerByPlayer>()
        .End(1)
        .GetRawEntities()[0];

      Move(_moveDirection);
      Jump();
      Crouch();
      Fire();
      Dash();

      Reset();
    }

    public void Destroy(IEcsSystems systems)
    {
      _input.OnMove -= x => _moveDirection = x;
      _input.OnJump -= () => _isJump = true;
      _input.OnCrouch -= () => _isCrouch = true;
      _input.OnFireLeft -= () => _nextHand = HandType.Left;
      _input.OnFireRight -= () => _nextHand = HandType.Right;
      _input.OnDash -= () => _isDash = true;
    }

    private void Move(float dir)
    {
      _world.Add<MoveCommand>(_player)
        .Direction = dir;
    }

    private void Jump()
    {
      if(_isJump)
        _world.Add<JumpCommand>(_player);
    }

    private void Crouch()
    {
      if(_isCrouch)
        _world.Add<CrouchCommand>(_player);
    }

    private void Fire()
    {
      if(_nextHand != HandType.None)
        _world.Add<FireCommand>(_player)
          .Hand = _nextHand;
    }

    private void Dash()
    {
      if (_isDash)
        _world.Add<DashCommand>(_player);
    }

    private void Reset()
    {
      _moveDirection = default;
      _isJump = default;
      _isCrouch = default;
      _nextHand = default;
      _isDash = default;
    }
  }
}