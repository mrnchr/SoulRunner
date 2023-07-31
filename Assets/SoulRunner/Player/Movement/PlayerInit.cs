using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerInit : MonoBehaviour
  {
    [SerializeField] private PlayerView _player;

    private void Awake()
    {
      HeroType startHero = _player.Chars.Hero.Current;
      _player.ShonMesh.enabled = startHero == HeroType.Shon;
      _player.KelliMesh.enabled = startHero == HeroType.Kelli;
    }

    private void Reset()
    {
      TryGetComponent(out _player);
    }
  }
}