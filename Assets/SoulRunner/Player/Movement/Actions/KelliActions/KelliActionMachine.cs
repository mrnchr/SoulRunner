using SoulRunner.Infrastructure;
using SoulRunner.Utility;

namespace SoulRunner.Player
{
  public class KelliActionMachine : PlayerActionMachine
  {
    private IFireAction _fire;
    private IDashAction _dash;
    private IKelliAttack _attack;

    protected override void Awake()
    {
      Owner = ObjectType.Kelli;
      _actions
        .AddItem(new KelliMoveAction(this))
        .AddItem(new PlayerJumpAction(this))
        .AddItem(new PlayerCrouchAction(this))
        .AddItem(new PlayerLandAction(this))
        .AddItem(new KelliFireAction(this))
        .AddItem(new KelliDashAction(this))
        .AddItem(new PlayerClimbAction(this))
        .AddItem(new PlayerClimbUpAction(this))
        .AddItem(new PlayerClimbDownAction(this))
        .AddItem(new PlayerFallAction(this))
        .AddItem(new KelliSwapAction(this))
        .AddItem(new KelliAttackAction(this));
      
      base.Awake();
    
      _fire = GetAction<IFireAction>();
      _dash = GetAction<IDashAction>();
      _attack = GetAction<IKelliAttack>();
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      View.GroundChecker.OnGroundEnter += _attack.Attack;
      
      _input.OnFireLeft += () => _fire.Fire(HandType.Left);
      _input.OnFireRight += () => _fire.Fire(HandType.Right);
      _input.OnMainAbility += _dash.Dash;
      _input.OnSideAbility += _attack.PlanAttack;
    }
    
    protected override void OnDisable()
    {
      base.OnDisable();
      View.GroundChecker.OnGroundEnter -= _attack.Attack;
      
      _input.OnFireLeft -= () => _fire.Fire(HandType.Left);
      _input.OnFireRight -= () => _fire.Fire(HandType.Right);
      _input.OnMainAbility -= _dash.Dash;
      _input.OnSideAbility -= _attack.PlanAttack;
    }
  }
}