using SoulRunner.Configuration.Anim;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunner.Player
{
  public enum PlayerAnimState
  {
    Idle = 0,
    Run = 1,
    Jump = 2
  }

  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private SkeletonAnimation _kelliSkeleton;
    [SerializeField] private SkeletonAnimation _shonSkeleton;
    [FormerlySerializedAs("_kelliAnims"),SerializeField] private KelliAnim _kelliAnim;
    [SerializeField] private ShonAnim _shonAnim;
    private SkeletonAnimation _activeSkeleton;

    private PlayerAnimState _currentState;

    private bool _isJump;

    private bool _isRun;

    public bool IsRun
    {
      get => _isRun;
      set => SetAnimVariable(value, ref _isRun);
    }

    public bool IsJump
    {
      get => _isJump;
      set => SetAnimVariable(value, ref _isJump);
    }

    private void Start()
    {
      _activeSkeleton = _kelliSkeleton;
    }

    private void SetAnimVariable<T>(T value, ref T variable)
    {
      bool needCheck = value.Equals(variable);
      variable = value;

      if (needCheck)
        CheckTransition();
    }

    private void CheckTransition()
    {
      switch (_currentState)
      {
        case PlayerAnimState.Idle when _isRun:
          Transit(PlayerAnimState.Run);
          break;
        case PlayerAnimState.Run when !_isRun:
          Transit(PlayerAnimState.Idle);
          break;
      }
    }

    private void Transit(PlayerAnimState toState)
    {
      switch (toState)
      {
        case PlayerAnimState.Idle:
          // _activeSkeleton.state.SetAnimation(0, _kelliAnim.idle, true);
          break;
        case PlayerAnimState.Run:
          // _activeSkeleton.state.SetAnimation(0, _kelliAnim.run, true);
          break;
      }

      _currentState = toState;
    }
  }
}