using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerAttackSystem : AttackSystem
  {
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerChars _chars;

    public override float GetDamagePoints()
    {
      return _chars.BaseDamage * _chars.BaseAttackRatio * _chars.GetChars<HeroAttackRatioChar>().Char;
    }

    public override float TakeDamage(View damaging, float damagePoints)
    {
      _chars.Health.Current -= damagePoints;
      return damagePoints;
    }
  }
}