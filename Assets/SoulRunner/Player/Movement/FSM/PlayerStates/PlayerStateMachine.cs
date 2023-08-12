using System.Linq;
using SoulRunner.Control;
using SoulRunner.Infrastructure;
using SoulRunner.Infrastructure.FSM;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerStateMachine : FinalStateMachine<PlayerView, PlayerStateType>
  {
    public bool IsActive;
    public ObjectType Owner;
    public PlayerStateVariables Variables;
    protected InputReader _input;
    protected PlayerState _moveState;
    protected PlayerState _swapState;

    [Inject]
    public virtual void Construct(InputReader input)
    {
      _input = input;
    }

    protected virtual void Awake()
    {
      Variables = View.StateVariables;
      foreach (var state in GetRawStates<PlayerState>().Append(_moveState).Append(_swapState))
        InitState(state);
      
      IsActive = View.Chars.Hero.Current == Owner;
      ReplaceState(PlayerStateType.Stand);
    }

    protected override void Start()
    {
      base.Start();
      _moveState.OnStart();
      _swapState.OnStart();
    }

    protected virtual void InitState(PlayerState state)
    {
      state.Machine = this;
      state.View = View;
    }

    protected virtual void OnEnable()
    {
      View.GroundChecker.OnGroundEnter += () => SetOnGround(true);
      View.GroundChecker.OnGroundExit += () => SetOnGround(false);
      
      View.LedgeChecker.OnLedgeEnter += x => SetOnLedge(true, x);
      View.LedgeChecker.OnLedgeExit += () => SetOnLedge(false);
      
      _input.OnInputRead += OnInputRead;
    }

    protected virtual void OnDisable()
    {
      View.GroundChecker.OnGroundEnter -= () => SetOnGround(true);
      View.GroundChecker.OnGroundExit -= () => SetOnGround(false);
      
      View.LedgeChecker.OnLedgeEnter -= x => SetOnLedge(true, x);
      View.LedgeChecker.OnLedgeExit -= () => SetOnLedge(false);

      _input.OnInputRead -= OnInputRead;
    }

    protected virtual void OnInputRead(InputValues inputs)
    {
      if (!IsActive) return;
      GetState<PlayerState>(CurrentState).ProcessInput(inputs);
      _moveState.ProcessInput(inputs);
      _swapState.ProcessInput(inputs);
    }

    protected virtual void SetOnLedge(bool isOnLedge, float posX = 0)
    {
      Variables.IsOnLedge = isOnLedge;
      Variables.LedgePosX = posX;
    }

    public virtual void SetOnGround(bool isOnGround)
    {
      Variables.IsOnGround = isOnGround;
      if (isOnGround)
        Variables.IsOnGroundEnter = true;
      else
        Variables.IsOnGroundExit = true;
    }

    protected override void Update()
    {
      base.Update();
      Variables.IsOnGroundEnter = false;
      Variables.IsOnGroundExit = false;
    }
  }
}