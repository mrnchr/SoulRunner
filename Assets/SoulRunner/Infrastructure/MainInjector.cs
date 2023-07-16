using SoulRunner.Configuration;
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
    public MainInjector(HeroConfig heroCfg,
                        InputReader inputReader,
                        IPlayerFactory playerFactory, 
                        Level.Level level, 
                        GroundCheckerService groundSvc, 
                        IFireballFactory fireballFactory,
                        FireballService fireballSvc) 
      : base(heroCfg, inputReader, playerFactory, level, groundSvc, fireballFactory, fireballSvc)
    {
    }
  }
}