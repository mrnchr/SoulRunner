using SoulRunner.Player;
using UnityEngine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class Spawner : MonoBehaviour
  {
    private LevelManagement.Level _level;
    private IPlayerFactory _factory;

    [Inject]    
    public void Construct(LevelManagement.Level level, IPlayerFactory factory)
    {
      _level = level;
      _factory = factory;
    }
    
    public void Start()
    {
      _factory.Create(_level.PlayerSpawn.position);
    }
  }
}