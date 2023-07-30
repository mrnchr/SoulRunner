using SoulRunner.Fireball;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class FireSystem : MonoBehaviour
  {
    [SerializeField] private PlayerView _player;
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
      fireballView.Rb.velocity = Vector2.right * (direction * fireballView.FireballCfg.Speed);
    }

    private void OnEnable()
    {
      _player.ActionVariables.OnFire += CastFireball;
    }

    private void OnDisable()
    {
      _player.ActionVariables.OnFire -= CastFireball;
    }
  }
}