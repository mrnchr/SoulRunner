using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballAnimSwitcher : MonoBehaviour
  {
    [SerializeField] private FireballView _view;
    [SerializeField] private FireballAnimator _animator;
    private FireballStateVariables _variables;

    private void Awake()
    {
      _variables = _view.StateVariables;
    }

    public void OnEnable()
    {
      _variables.OnDeathStart += AnimateDeath;
    }

    public void OnDisable()
    {
      _variables.OnDeathStart -= AnimateDeath;
    }

    private void AnimateDeath() => _animator.DeathTrigger = true;
  }
}