using SoulRunner.Player;
using UnityEngine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class Spawner : MonoBehaviour
  {
    private Level.Level _level;
    private IPlayerFactory _factory;

    [Inject]    
    public void Construct(Level.Level level, IPlayerFactory factory)
    {
      _level = level;
      _factory = factory;
    }
    
    private void Start()
    {
      _factory.Create(_level.PlayerSpawn.position);
    }
  }
}