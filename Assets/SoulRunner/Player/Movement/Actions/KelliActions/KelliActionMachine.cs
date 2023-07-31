using SoulRunner.Utility;

namespace SoulRunner.Player
{
  public class KelliActionMachine : PlayerActionMachine
  {
    private IFireAction _fire;
    private IDashAction _dash;
    
    protected override void Awake()
    {
      Owner = HeroType.Kelli;
      _actions
        .AddItem(new PlayerMoveAction(this))
        .AddItem(new PlayerJumpAction(this))
        .AddItem(new PlayerCrouchAction(this))
        .AddItem(new PlayerLandAction(this))
        .AddItem(new KelliFireAction(this))
        .AddItem(new KelliDashAction(this))
        .AddItem(new PlayerClimbAction(this))
        .AddItem(new PlayerClimbUpAction(this))
        .AddItem(new PlayerClimbDownAction(this))
        .AddItem(new PlayerFallAction(this))
        .AddItem(new KelliSwapAction(this));
      
      base.Awake();
    
      _fire = GetAction<IFireAction>();
      _dash = GetAction<IDashAction>();
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      _input.OnFireLeft += () => _fire.Fire(HandType.Left);
      _input.OnFireRight += () => _fire.Fire(HandType.Right);
      _input.OnDash += _dash.Dash;
    }
    
    protected override void OnDisable()
    {
      base.OnDisable();
      _input.OnFireLeft -= () => _fire.Fire(HandType.Left);
      _input.OnFireRight -= () => _fire.Fire(HandType.Right);
      _input.OnDash -= _dash.Dash;
    }
  }
}