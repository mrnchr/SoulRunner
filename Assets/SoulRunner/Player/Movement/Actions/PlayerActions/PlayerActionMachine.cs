using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunner.Control;
using SoulRunner.Infrastructure.Actions;
using SoulRunner.Utility;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerActionMachine : ActionMachine<PlayerView>
  {
    private PlayerActionVariables _variables;
    private InputReader _input;
    
    private IMoveAction _move;
    private IJumpAction _kelliJump;
    private IJumpAction _shonJump;
    private ICrouchAction _crouch;
    private ILandAction _land;
    private IClimbAction _climb;
    private IClimbUpAction _climbUp;
    private IClimbDownAction _climbDown;
    private ISwapAction _kelliSwap;
    private ISwapAction _shonSwap;
    private IDashAction _dash;
    private IFireAction _fire;

    [Inject]
    public virtual void Construct(InputReader input)
    {
      _input = input;
    }

    protected virtual void Awake()
    {
      _variables = _view.ActionVariables;
      _actions
        .AddItem(new PlayerMoveAction(_view))
        .AddItem(new KelliJumpAction(_view))
        .AddItem(new ShonJumpAction(_view))
        .AddItem(new PlayerCrouchAction(_view))
        .AddItem(new PlayerLandAction(_view))
        .AddItem(new KelliFireAction(_view))
        .AddItem(new KelliDashAction(_view))
        .AddItem(new PlayerClimbAction(_view))
        .AddItem(new PlayerClimbUpAction(_view))
        .AddItem(new PlayerClimbDownAction(_view))
        .AddItem(new PlayerFallAction(_view))
        .AddItem(new KelliSwapAction(_view))
        .AddItem(new ShonSwapAction(_view));

      _move = GetAction<IMoveAction>();
      _kelliJump = GetAction<IKelliAction, IJumpAction>();
      _shonJump = GetAction<IShonAction, IJumpAction>();
      _crouch = GetAction<ICrouchAction>();
      _land = GetAction<ILandAction>();
      _climb = GetAction<IClimbAction>();
      _climbUp = GetAction<IClimbUpAction>();
      _climbDown = GetAction<IClimbDownAction>();
      _dash = GetAction<IDashAction>();
      _fire = GetAction<IFireAction>();
      _kelliSwap = GetAction<IKelliAction, ISwapAction>();
      _shonSwap = GetAction<IShonAction, ISwapAction>();
      
      ChangeHeroActions();
    }

    private void OnEnable()
    {
      _view.GroundChecker.OnGroundEnter += () => _variables.IsOnGround = true;
      _view.GroundChecker.OnGroundExit += () => _variables.IsOnGround = false;
      _view.GroundChecker.OnGroundEnter += _land.Land;
      
      _view.LedgeChecker.OnLedgeEnter += x => SetOnLedge(true, x);
      _view.LedgeChecker.OnLedgeExit += () => SetOnLedge(false);
      
      _input.OnMove += _move.Move;
      _input.OnJump += _kelliJump.Jump;
      _input.OnJump += _shonJump.Jump;
      _input.OnCrouch += _crouch.Crouch;
      _input.OnGrab += _climb.Climb;
      _input.OnClimbUp += _climbUp.ClimbUp;
      _input.OnClimbDown += _climbDown.ClimbDown;
      _input.OnSwapHero += _kelliSwap.Swap;
      _input.OnSwapHero += _shonSwap.Swap;
      _input.OnFireLeft += () => _fire.Fire(HandType.Left);
      _input.OnFireRight += () => _fire.Fire(HandType.Right);
      _input.OnDash += _dash.Dash;

      _variables.OnSwap += _ => ChangeHeroActions();
    }

    private void OnDisable()
    {
      _view.GroundChecker.OnGroundEnter -= () => _variables.IsOnGround = true;
      _view.GroundChecker.OnGroundExit -= () => _variables.IsOnGround = false;
      _view.GroundChecker.OnGroundEnter -= _land.Land;
      
      _view.LedgeChecker.OnLedgeEnter -= x => SetOnLedge(true, x);
      _view.LedgeChecker.OnLedgeExit -= () => SetOnLedge(false);
      
      _input.OnMove -= _move.Move;
      _input.OnJump -= _kelliJump.Jump;
      _input.OnJump -= _shonJump.Jump;
      _input.OnCrouch -= _crouch.Crouch;
      _input.OnGrab -= _climb.Climb;
      _input.OnClimbUp -= _climbUp.ClimbUp;
      _input.OnClimbDown -= _climbDown.ClimbDown;
      _input.OnSwapHero -= _kelliSwap.Swap;
      _input.OnSwapHero -= _shonSwap.Swap;
      _input.OnFireLeft -= () => _fire.Fire(HandType.Left);
      _input.OnFireRight -= () => _fire.Fire(HandType.Right);
      _input.OnDash -= _dash.Dash;
      
      _variables.OnSwap -= _ => ChangeHeroActions();
    }

    protected virtual void ChangeHeroActions()
    {
      var kelli = GetRawActions<IKelliAction, ICycleAction>().ToList();
      var shon = GetRawActions<IShonAction, ICycleAction>().ToList();
      if (_variables.ActiveHero == HeroType.Kelli)
        Change(kelli, shon);
      else
        Change(shon, kelli);
      
      void Change(List<ICycleAction> activated, List<ICycleAction> deactivated)
      {
        activated.ForEach(x => x.Activate());
        deactivated.ForEach(x => x.Deactivate());
      }
    }

    protected virtual void SetOnLedge(bool isOnLedge, float posX = 0)
    {
      _variables.IsOnLedge = isOnLedge;
      _variables.LedgePosX = posX;
    }
    
    protected virtual void Reset()
    {
      TryGetComponent(out _view);
    }
  }
}