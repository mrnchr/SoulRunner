using System.Linq;
using SoulRunner.Control;
using SoulRunner.Infrastructure.Actions;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerActionMachine : ActionMachine<PlayerView>
  {
    public HeroType Owner { get; protected set; }
    
    protected PlayerActionVariables _variables;
    protected PlayerChars _chars;
    protected InputReader _input;
    
    private IMoveAction _move;
    private IJumpAction _jump;
    private ILandAction _land;
    private ICrouchAction _crouch;
    private IClimbAction _climb;
    private IClimbUpAction _climbUp;
    private IClimbDownAction _climbDown;
    private ISwapAction _swap;

    [Inject]
    public virtual void Construct(InputReader input)
    {
      _input = input;
    }

    public virtual void ActivateActions()
    {
      GetRawActions<ICycleAction>().ToList().ForEach(x => x.Activate());
    }

    public virtual void DeactivateActions()
    {
      GetRawActions<ICycleAction>().ToList().ForEach(x => x.Deactivate());
    }

    public virtual void ChangeActionActivity(HeroType hero)
    {
      if(hero == Owner)
        ActivateActions();
      else
        DeactivateActions();
    }

    protected virtual void Awake()
    {
      _variables = View.ActionVariables;
      _chars = View.Chars;

      _move = GetAction<IMoveAction>();
      _jump = GetAction<IJumpAction>();
      _crouch = GetAction<ICrouchAction>();
      _land = GetAction<ILandAction>();
      _climb = GetAction<IClimbAction>();
      _climbUp = GetAction<IClimbUpAction>();
      _climbDown = GetAction<IClimbDownAction>();
      _swap = GetAction<ISwapAction>();
    }

    protected override void Start()
    {
      ChangeActionActivity(_chars.Hero);
      base.Start();
    }

    protected virtual void OnEnable()
    {
      View.GroundChecker.OnGroundEnter += () => _variables.IsOnGround = true;
      View.GroundChecker.OnGroundExit += () => _variables.IsOnGround = false;
      View.GroundChecker.OnGroundEnter += _land.Land;
      
      View.LedgeChecker.OnLedgeEnter += x => SetOnLedge(true, x);
      View.LedgeChecker.OnLedgeExit += () => SetOnLedge(false);
      
      _input.OnMove += _move.Move;
      _input.OnJump += _jump.Jump;
      _input.OnCrouch += _crouch.Crouch;
      _input.OnGrab += _climb.Climb;
      _input.OnClimbUp += _climbUp.ClimbUp;
      _input.OnClimbDown += _climbDown.ClimbDown;
      _input.OnSwapHero += _swap.Swap;

      _variables.OnSwap += ChangeActionActivity;
    }

    protected virtual void OnDisable()
    {
      View.GroundChecker.OnGroundEnter -= () => _variables.IsOnGround = true;
      View.GroundChecker.OnGroundExit -= () => _variables.IsOnGround = false;
      View.GroundChecker.OnGroundEnter -= _land.Land;
      
      View.LedgeChecker.OnLedgeEnter -= x => SetOnLedge(true, x);
      View.LedgeChecker.OnLedgeExit -= () => SetOnLedge(false);
      
      _input.OnMove -= _move.Move;
      _input.OnJump -= _jump.Jump;
      _input.OnCrouch -= _crouch.Crouch;
      _input.OnGrab -= _climb.Climb;
      _input.OnClimbUp -= _climbUp.ClimbUp;
      _input.OnClimbDown -= _climbDown.ClimbDown;
      _input.OnSwapHero -= _swap.Swap;
      
      _variables.OnSwap -= ChangeActionActivity;
    }

    protected virtual void SetOnLedge(bool isOnLedge, float posX = 0)
    {
      _variables.IsOnLedge = isOnLedge;
      _variables.LedgePosX = posX;
    }
    
    protected virtual void Reset()
    {
      TryGetComponent(out View);
    }
  }
}