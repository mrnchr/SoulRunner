using UnityEngine;

namespace SoulRunner.Fireball
{
  public class FireballAnimSwitcher : MonoBehaviour
  {
    [SerializeField] private FireballView _view;
    private FireballAnimator _animator;
    private FireballActionVariables _variables;

    private void Awake()
    {
      _animator = _view.Anim;
      _variables = _view.ActionVariables;
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