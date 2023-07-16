using System;
using SoulRunner.Infrastructure;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class PlayerFactory : IPlayerFactory
  {
    public Action<PlayerView> OnPlayerCreated { get; set; }
    
    private readonly DiContainer _container;
    private readonly PlayerView _playerPrefab;

    [Inject]
    public PlayerFactory(DiContainer container, PrefabService prefabSvc)
    {
      _container = container;
      _playerPrefab = prefabSvc.Prefabs.Player;
    }

    public PlayerView Create(Vector3 at)
    {
      var player = _container
        .InstantiatePrefabForComponent<PlayerView>(_playerPrefab, at, Quaternion.identity, null);
      OnPlayerCreated(player);
      return player;
    }
  }

  public interface IPlayerFactory : IFactory<Vector3, PlayerView>
  {
    public Action<PlayerView> OnPlayerCreated { get; set; }
  }
}