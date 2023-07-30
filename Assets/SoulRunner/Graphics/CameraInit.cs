using Com.LuisPedroFonseca.ProCamera2D;
using SoulRunner.Player;
using UnityEngine;
using Zenject;

namespace SoulRunner
{
  public class CameraInit : MonoBehaviour
  {
    [SerializeField] private ProCamera2D _proCamera;
    private IPlayerFactory _playerFactory;

    [Inject]
    public void Construct(IPlayerFactory playerFactory)
    {
      _playerFactory = playerFactory;
    }

    private void OnEnable()
    {
      _playerFactory.OnPlayerCreated += SetPlayerAsTarget;
    }

    private void OnDisable()
    {
      _playerFactory.OnPlayerCreated -= SetPlayerAsTarget;
    }

    private void SetPlayerAsTarget(PlayerView player)
    { 
      _proCamera.AddCameraTarget(player.transform);
    }

    private void Reset()
    {
      TryGetComponent(out _proCamera);
    }
  }
}