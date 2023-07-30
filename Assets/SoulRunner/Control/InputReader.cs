using System;
using Rewired;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Control
{
  public class InputReader : MonoBehaviour
  {
    private Rewired.Player _player;
    public Action<float> OnMove;
    public Action OnJump;
    public Action OnCrouch;
    public Action OnClimbUp;
    public Action OnClimbDown;
    public Action OnGrab;
    public Action OnFireLeft;
    public Action OnFireRight;
    public Action OnMainAbility;
    public Action OnDash;
    public Action OnSwapHero;
    public Action OnPickUp;
    public Action OnUse;
    public Action OnPrevItem;
    public Action OnNextItem;

    private void Awake()
    {
      _player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
      OnMove?.Invoke(_player.GetAxis(Idents.InputActions.Horizontal));
      ReadButtonDown(Idents.InputActions.Up, OnJump);
      ReadButton(Idents.InputActions.Down, OnCrouch);
      ReadButtonDown(Idents.InputActions.Up, OnClimbUp);
      ReadButtonDown(Idents.InputActions.Down, OnClimbDown);
      ReadButtonDown(Idents.InputActions.Up, OnGrab);
      ReadButton(Idents.InputActions.FireLeft, OnFireLeft);
      ReadButton(Idents.InputActions.FireRight, OnFireRight);
      ReadButton(Idents.InputActions.MainAbility, OnMainAbility);
      ReadButton(Idents.InputActions.SideAbility, OnDash);
      ReadButtonDown(Idents.InputActions.SwapHero, OnSwapHero);
      ReadButton(Idents.InputActions.PickUp, OnPickUp);
      ReadButton(Idents.InputActions.UseItem, OnUse);
      ReadButton(Idents.InputActions.NextItem, OnNextItem);
      ReadButton(Idents.InputActions.PrevItem, OnPrevItem);
    }

    private void ReadButton(string inputAction, Action action)
    {
      if (_player.GetButton(inputAction))
        action?.Invoke();
    }
    
    private void ReadButtonDown(string inputAction, Action action)
    {
      if (_player.GetButtonDown(inputAction))
        action?.Invoke();
    }
  }
}