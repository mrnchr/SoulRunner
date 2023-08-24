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

    public Action<InputValues> OnInputRead;
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
      
      ReadButtonDown(Idents.InputActions.Up, OnJump, out Values.JumpButton);
      ReadButton(Idents.InputActions.Down, OnCrouch, out Values.CrouchButton);
      ReadButtonDown(Idents.InputActions.Up, OnClimbUp, out Values.ClimbUpButton);
      ReadButtonDown(Idents.InputActions.Down, OnClimbDown, out Values.ClimbDownButton);
      ReadButtonDown(Idents.InputActions.Up, OnGrab, out Values.GrabButton);
      ReadButton(Idents.InputActions.FireLeft, OnFireLeft, out Values.FireLeftButton);
      ReadButton(Idents.InputActions.FireRight, OnFireRight, out Values.FireRightButton);
      ReadButton(Idents.InputActions.MainAbility, OnMainAbility, out Values.MainAbilityButton);
      ReadButtonDown(Idents.InputActions.SideAbility, OnSideAbility, out Values.SideAbilityButton);
      ReadButtonDown(Idents.InputActions.SwapHero, OnSwapHero, out Values.SwapHeroButton);
      ReadButton(Idents.InputActions.PickUp, OnPickUp, out Values.PickUpButton);
      ReadButton(Idents.InputActions.UseItem, OnUse, out Values.UseItemButton);
      ReadButton(Idents.InputActions.NextItem, OnNextItem, out Values.NextItemButton);
      ReadButton(Idents.InputActions.PrevItem, OnPrevItem, out Values.PrevItemButton);

      OnInputRead?.Invoke(Values);
    }

    private void ReadButton(string inputAction, Action action, out bool button)
    {
      button = _player.GetButton(inputAction);
      if (button)
        action?.Invoke();
    }

    private void ReadButtonDown(string inputAction, Action action, out bool button)
    {
      button = _player.GetButtonDown(inputAction);
      if (button)
        action?.Invoke();
    }
  }
}