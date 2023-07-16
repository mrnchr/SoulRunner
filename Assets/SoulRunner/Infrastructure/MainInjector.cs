using SoulRunner.Configuration;
using SoulRunner.Control;
using SoulRunner.Player;
using SoulRunner.Utility.Ecs.Combine;
using Zenject;

namespace SoulRunner.Infrastructure
{
  public class MainInjector : EcsCombineInjector
  {
    [Inject]
    public MainInjector(Hero hero, InputReader inputReader, IPlayerFactory playerFactory, Level.Level level, GroundCheckerService groundSvc) 
      : base(hero, inputReader, playerFactory, level, groundSvc)
    {
    }
  }
}