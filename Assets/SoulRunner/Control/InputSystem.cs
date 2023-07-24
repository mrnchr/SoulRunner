using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Player;
using SoulRunner.Player.Movement;
using SoulRunner.Utility.Ecs;

namespace SoulRunner.Control
{
  public class InputSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private readonly EcsCustomInject<InputReader> _inputReader = default;
    private InputReader _input;
    private int _player;
    private EcsWorld _world;

    private float _moveDirection;
    private bool _isUp;
    private bool _isDown;
    private HandType _nextHand;
    private bool _isDash;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
      _input = _inputReader.Value;
      _input.OnMove += x => _moveDirection = x;
      _input.OnJump += () => _isUp = true;
      _input.OnCrouch += () => _isDown = true;
      _input.OnFireLeft += () => _nextHand = HandType.Left;
      _input.OnFireRight += () => _nextHand = HandType.Right;
      _input.OnDash += () => _isDash = true;
      _input.OnClimbUp += () => _isUp = true;
      _input.OnClimbDown += () => _isDown = true;
    }

    public void Run(IEcsSystems systems)
    {
      _player = _world
        .Filter<ControllerByPlayer>()
        .End(1)
        .GetRawEntities()[0];

      Move(_moveDirection);
      Up();
      Down();
      Fire();
      Dash();

      Reset();
    }

    public void Destroy(IEcsSystems systems)
    {
      _input.OnMove -= x => _moveDirection = x;
      _input.OnJump -= () => _isUp = true;
      _input.OnCrouch -= () => _isDown = true;
      _input.OnFireLeft -= () => _nextHand = HandType.Left;
      _input.OnFireRight -= () => _nextHand = HandType.Right;
      _input.OnDash -= () => _isDash = true;
      _input.OnClimbUp -= () => _isUp = true;
      _input.OnClimbDown -= () => _isDown = true;
    }

    private void Move(float dir)
    {
      _world.Add<MoveCommand>(_player)
        .Direction = dir;
    }

    private void Up()
    {
      if(_isUp)
        _world.Add<UpCommand>(_player);
    }

    private void Down()
    {
      if(_isDown)
        _world.Add<DownCommand>(_player);
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
      _isUp = default;
      _isDown = default;
      _nextHand = default;
      _isDash = default;
    }
  }
}