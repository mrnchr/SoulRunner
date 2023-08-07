using System;
using Rewired;
using SoulRunner.Utility;
using Zenject;

namespace SoulRunner.Control
{
  public class InputReader : IInitializable, ITickable
  {
    public Action<float> OnMove;
    public Action OnJump;
    public Action OnCrouch;
    public Action OnClimbUp;
    public Action OnClimbDown;
    public Action OnGrab;
    public Action OnFireLeft;
    public Action OnFireRight;
    public Action OnMainAbility;
    public Action OnSideAbility;
    public Action OnSwapHero;
    public Action OnPickUp;
    public Action OnUse;
    public Action OnPrevItem;
    public Action OnNextItem;

    public InputValues Values;

    private Rewired.Player _player;

    public void Initialize()
    {
      _player = ReInput.players.GetPlayer(0);
    }

    public void Tick()
    {
      float moveAxis = _player.GetAxis(Idents.InputActions.Horizontal);
      OnMove?.Invoke(moveAxis);
      Values.MoveDirection = moveAxis;
      
      ReadButtonDown(Idents.InputActions.Up, OnJump, ref Values.JumpButton);
      ReadButton(Idents.InputActions.Down, OnCrouch, ref Values.CrouchButton);
      ReadButtonDown(Idents.InputActions.Up, OnClimbUp, ref Values.ClimbUpButton);
      ReadButtonDown(Idents.InputActions.Down, OnClimbDown, ref Values.ClimbDownButton);
      ReadButtonDown(Idents.InputActions.Up, OnGrab, ref Values.GrabButton);
      ReadButton(Idents.InputActions.FireLeft, OnFireLeft, ref Values.FireLeftButton);
      ReadButton(Idents.InputActions.FireRight, OnFireRight, ref Values.FireRightButton);
      ReadButton(Idents.InputActions.MainAbility, OnMainAbility, ref Values.MainAbilityButton);
      ReadButtonDown(Idents.InputActions.SideAbility, OnSideAbility, ref Values.SideAbilityButton);
      ReadButtonDown(Idents.InputActions.SwapHero, OnSwapHero, ref Values.SwapHeroButton);
      ReadButton(Idents.InputActions.PickUp, OnPickUp, ref Values.PickUpButton);
      ReadButton(Idents.InputActions.UseItem, OnUse, ref Values.UseItemButton);
      ReadButton(Idents.InputActions.NextItem, OnNextItem, ref Values.NextItemButton);
      ReadButton(Idents.InputActions.PrevItem, OnPrevItem, ref Values.PrevItemButton);
    }

    private void ReadButton(string inputAction, Action action, ref bool button)
    {
      button = _player.GetButton(inputAction);
      if (button)
      {
        action?.Invoke();
      }
    }

    private void ReadButtonDown(string inputAction, Action action, ref bool button)
    {
      button = _player.GetButtonDown(inputAction);
      if (button)
      {
        action?.Invoke();
      }
    }
  }
}