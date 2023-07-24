using SoulRunner.Control;
using SoulRunner.Fireball;
using SoulRunner.Player;
using SoulRunner.Utility.Ecs.Combine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class MainInjector : EcsCombineInjector
  {
    [Inject]
    public MainInjector(PlayerConfig playerCfg,
                        InputReader inputReader,
                        IPlayerFactory playerFactory, 
                        Level.Level level, 
                        IFireballFactory fireballFactory,
                        FireballService fireballSvc) 
      : base(playerCfg, inputReader, playerFactory, level, fireballFactory, fireballSvc)
    {
    }
  }
}