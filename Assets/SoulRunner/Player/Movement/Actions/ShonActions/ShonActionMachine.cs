using SoulRunner.Infrastructure;
using SoulRunner.Utility;

namespace SoulRunner.Player
{
  public class ShonActionMachine : PlayerActionMachine
  {
    protected override void Awake()
    {
      Owner = ObjectType.Shon;
      _actions
        .AddItem(new PlayerMoveAction(this))
        .AddItem(new ShonJumpAction(this))
        .AddItem(new PlayerCrouchAction(this))
        .AddItem(new ShonLandAction(this))
        .AddItem(new PlayerClimbAction(this))
        .AddItem(new PlayerClimbUpAction(this))
        .AddItem(new PlayerClimbDownAction(this))
        .AddItem(new PlayerFallAction(this))
        .AddItem(new ShonSwapAction(this));
      
      base.Awake();
    }
  }
}