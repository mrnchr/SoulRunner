using SoulRunner.Infrastructure;
using UnityEngine;

namespace SoulRunner.Player
{
  public class PlayerInit : MonoBehaviour
  {
    [SerializeField] private PlayerView _player;

    private void Awake()
    {
      ObjectType startHero = _player.Chars.Hero.Current;
      _player.ShonMesh.enabled = startHero == ObjectType.Shon;
      _player.KelliMesh.enabled = startHero == ObjectType.Kelli;
    }

    private void Reset()
    {
      TryGetComponent(out _player);
    }
  }
}