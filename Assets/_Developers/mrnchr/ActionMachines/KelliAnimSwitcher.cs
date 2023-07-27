using UnityEngine;

namespace SoulRunner.Player.ActionMachines
{
  public class PlayerAnimSwitcher : MonoBehaviour
  {
    [SerializeField] private PlayerView _player;
    private KelliActionMachine _machine;

    private void Awake()
    {
      // _machine = _player.CurrentMachine;
    }

    private void OnEnable()
    {
      _machine.GetAction<IMoveAction>().OnStart += AnimateMove;
      _machine.GetAction<IJumpAction, IStartAction>().OnStart += AnimateJump;
      _machine.GetAction<ILandAction, IEndAction>().OnEnd += AnimateLand;

    }

    private void OnDisable()
    {
      _machine.GetAction<IMoveAction>().OnStart -= AnimateMove;
      _machine.GetAction<IJumpAction, IStartAction>().OnStart -= AnimateJump;
      _machine.GetAction<ILandAction, IEndAction>().OnEnd -= AnimateLand;
    }

    private void AnimateMove(float direction)
    {
      _player.CurrentAnimator.IsRun = direction != 0;
      transform.localScale = SetViewDirection(transform.localScale, direction);
    }

    private void AnimateJump()
    {
      _player.CurrentAnimator.IsJump = true;
    }

    private void AnimateLand()
    {
      _player.CurrentAnimator.IsJump = false;
    }

    private Vector3 SetViewDirection(Vector3 scale, float moveDirection)
    {
      if (moveDirection * scale.x < 0)
        scale.x *= -1;
      return scale;
    }

    private void Reset()
    {
      TryGetComponent(out _player);
    }
  }
}