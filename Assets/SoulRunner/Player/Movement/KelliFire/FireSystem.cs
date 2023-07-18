using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SoulRunner.Fireball;
using SoulRunner.Player.Fireball;
using SoulRunner.Utility.Ecs;
using Spine.Unity;
using UnityEngine;

namespace SoulRunner.Player.Movement
{
  public class FireSystem : IEcsRunSystem, IEcsInitSystem
  {
    private readonly EcsFilterInject<Inc<FireTimer, PlayerViewRef>> _fire = default;
    private readonly EcsCustomInject<FireballService> _fireballSvc = default;
    private readonly EcsCustomInject<IFireballFactory> _fireballFactory = default;
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems)
    {
      foreach (int index in _fire.Value)
      {
        if (_world.Get<FireTimer>(index).TimeLeft <= 0)
        {
          int fireball = _fireballSvc.Value.Create();
          
          PlayerView playerView = _world.Get<PlayerViewRef>(index).Value;
          BoneFollower hand = playerView.GetHand(_world.Get<FireTimer>(index).Hand);
          FireballView fireballView = _fireballFactory.Value.Create(hand.transform.position);
          
          _world.Add<FireballViewRef>(fireball)
            .Value = fireballView;
          fireballView.Entity = fireball;

          float direction = Mathf.Sign(hand.transform.position.x - playerView.transform.position.x);
          fireballView.transform.localScale = new Vector3(direction,1, 1);
          fireballView.Rb.velocity = Vector3.right * direction * _world.Get<Movable>(fireball).Speed;
          
          _world.Del<FireTimer>(index);
        }
      }
    }
  }
}