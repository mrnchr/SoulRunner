using System.Collections.Generic;
using System.Linq;
using SoulRunner.Control;
using SoulRunner.Utility;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player.ActionMachines
{
  public class ActionMachine : MonoBehaviour, IJumpMachine
  {
    public bool IsOnGround { get; set; }
    public bool IsInJump { get; set; }
    public bool IsCrouch { get; set; }

    private readonly List<MovementAction<PlayerView>> _actions = new List<MovementAction<PlayerView>>();
    private IMoveAction _move;
    private IJumpAction _jump;
    private ICrouchAction _crouch;

    [SerializeField] private PlayerView _player;
    private InputReader _input;

    [Inject]
    public void Construct(InputReader input)
    {
      _input = input;
    }
    
    private void Awake()
    {
      _actions
        .AddItem(new KelliMoveAction(_player))
        .AddItem(new KelliJumpAction(_player))
        .AddItem(new KelliCrouchAction(_player));

      _move = GetAction<IMoveAction>();
      _jump = GetAction<IJumpAction>();
      _crouch = GetAction<ICrouchAction>();
    }

    private void OnEnable()
    {
      _input.OnMove += _move.Move;
      _input.OnJump += _jump.Jump;
      _input.OnCrouch += _crouch.Crouch;
    }

    private void OnDisable()
    {
      _input.OnMove -= _move.Move;
      _input.OnJump -= _jump.Jump;
      _input.OnCrouch -= _crouch.Crouch;
    }

    public T GetAction<T>() => GetActions<T>()[0];
    public T2 GetAction<T1, T2>() => GetActions<T1, T2>()[0];
    public T3 GetAction<T1, T2, T3>() => GetActions<T1, T2, T3>()[0];
    public T4 GetAction<T1, T2, T3, T4>() => GetActions<T1, T2, T3, T4>()[0];
    
    public T[] GetActions<T>() => GetRawActions<T>().ToArray();
    public T2[] GetActions<T1, T2>() => GetRawActions<T1, T2>().ToArray();
    public T3[] GetActions<T1, T2, T3>() => GetRawActions<T1, T2, T3>().ToArray();
    public T4[] GetActions<T1, T2, T3, T4>() => GetRawActions<T1, T2, T3, T4>().ToArray();

    private IEnumerable<T> GetRawActions<T>() => _actions.OfType<T>();
    private IEnumerable<T2> GetRawActions<T1, T2>() => GetRawActions<T1>().OfType<T2>();
    private IEnumerable<T3> GetRawActions<T1, T2, T3>() => GetRawActions<T1, T2>().OfType<T3>();
    private IEnumerable<T4> GetRawActions<T1, T2, T3, T4>() => GetRawActions<T1, T2, T3>().OfType<T4>();

    private void Reset()
    {
      TryGetComponent(out _player);
    }
  }
}