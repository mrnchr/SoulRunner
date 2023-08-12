using SoulRunner.Fireball;
using SoulRunner.Infrastructure;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class FireSystem : MonoBehaviour
  {
    [SerializeField] private PlayerView _player;
    [SerializeField] private AttackSystem _attackSys;
    private IFireballFactory _fireballFactory;

    [Inject]
    public void Construct(IFireballFactory fireballFactory)
    {
      _fireballFactory = fireballFactory;
    }

    public void CastFireball(HandType fireHand)
    {
      Transform hand = _player.GetHand(fireHand).transform;
      Vector3 handPos = hand.position;
      float direction = Mathf.Sign(handPos.x - _player.transform.position.x);
      
      FireballView fireballView = _fireballFactory.Create(handPos);
      fireballView.transform.localScale = new Vector3(direction, 1, 1);
      fireballView.Rb.velocity = Vector2.right * (direction * _player.Chars.FireballSpeed);
      fireballView.Weapon.Owner = _player;
      fireballView.Weapon.AttackPoints = _attackSys.GetDamagePoints();
    }

    private void OnEnable()
    {
      _player.StateVariables.OnFire += CastFireball;
    }

    private void OnDisable()
    {
      _player.StateVariables.OnFire -= CastFireball;
    }
  }
}