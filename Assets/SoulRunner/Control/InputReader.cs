using System;
using Rewired;
using SoulRunner.Utility;
using UnityEngine;

namespace SoulRunner.Control
{
  public class InputReader : MonoBehaviour
  {
    private Rewired.Player _player;
    public Action OnAbility1;
    public Action OnAbility2;
    public Action OnCrouch;
    public Action OnFireLeft;
    public Action OnFireRight;
    public Action OnJump;
    public Action<float> OnMove;
    public Action OnNextItem;
    public Action OnPickUp;
    public Action OnPrevItem;
    public Action OnSwapHero;
    public Action OnUse;

    private void Awake()
    {
      _player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
      OnMove?.Invoke(_player.GetAxis(Idents.InputActions.Horizontal));
      ReadInput(Idents.InputActions.Jump, OnJump);
      ReadInput(Idents.InputActions.Crouch, OnCrouch);
      ReadInput(Idents.InputActions.FireLeft, OnFireLeft);
      ReadInput(Idents.InputActions.FireRight, OnFireRight);
      ReadInput(Idents.InputActions.MainAbility, OnAbility1);
      ReadInput(Idents.InputActions.SideAbility, OnAbility2);
      ReadInput(Idents.InputActions.PickUp, OnPickUp);
      ReadInput(Idents.InputActions.UseItem, OnUse);
      ReadInput(Idents.InputActions.SwapHero, OnSwapHero);
      ReadInput(Idents.InputActions.NextItem, OnNextItem);
      ReadInput(Idents.InputActions.PrevItem, OnPrevItem);
    }

    private void ReadInput(string inputAction, Action action)
    {
      if (_player.GetButton(inputAction))
        action?.Invoke();
    }
  }
}