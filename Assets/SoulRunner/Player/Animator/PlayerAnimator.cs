using System;
using SoulRunner.Infrastructure.Spine;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerAnimator<TEnum> : MonoSpineAnimator<TEnum>
    where TEnum : Enum
  {
    public bool IsRun
    {
      get => _isRun;
      set => SetVariable(value, ref _isRun);
    }


    public bool IsJump
    {
      get => _isJump;
      set => SetVariable(value, ref _isJump);
    }


    public bool IsCrouch
    {
      get => _isCrouch;
      set => SetVariable(value, ref _isCrouch);
    }


    public bool IsClimb
    {
      get => _isClimb;
      set => SetVariable(value, ref _isClimb);
    }


    public bool IsFall
    {
      get => _isFall;
      set => SetVariable(value, ref _isFall);
    }

    public bool SwapTrigger
    {
      private get => _swapTrigger;
      set => SetVariable(value, ref _swapTrigger);
    }
    
#if UNITY_EDITOR
    [SerializeField] protected string _currentAnim;
#endif

    [SerializeField] protected bool _isRun;
    [SerializeField] protected bool _isJump;
    [SerializeField] protected bool _isFall;
    [SerializeField] protected bool _isCrouch;
    [SerializeField] protected bool _isClimb;
    
    [SerializeField] protected bool _swapTrigger;
    
    protected override void Update()
    {
      base.Update();
      
#if UNITY_EDITOR
      _currentAnim = _animator.GetLayer(0).Current.Animation.Name.ToString();
#endif
    }

    protected override void ClearTriggers()
    {
      _swapTrigger = false;
    }
  }
}