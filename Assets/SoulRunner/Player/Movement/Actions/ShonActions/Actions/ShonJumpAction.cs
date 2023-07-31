using SoulRunner.Infrastructure.Actions;

namespace SoulRunner.Player
{
  public class ShonJumpAction : PlayerJumpAction, IShonAction, IStartAction
  {
    private ILandAction _land;

    public ShonJumpAction(ActionMachine<PlayerView> machine) : base(machine)
    {
    }

    public void Start()
    {
      _land = _machine.GetAction<IShonAction, ILandAction>();
    }

    public override void Activate()
    {
      base.Activate();
      _variables.CanAirJump = true;
    }

    public override void Jump()
    {
      if (!IsActive) return;

      if (_variables.CanAirJump && !_variables.IsOnLedge)
      {
        if (_variables.IsFalling)
        {
          _land.Land();
          AirJumpForcefully();
        }
        else if (_variables.IsJumping)
        {
          AirJumpForcefully();
          return;
        }
      }

      base.Jump();
    }

    public void AirJumpForcefully()
    {
      JumpForcefully();
      _variables.CanAirJump = false;
      _variables.OnAirJump?.Invoke();
    }
  }
}