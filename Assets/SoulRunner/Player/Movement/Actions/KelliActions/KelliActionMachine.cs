// using SoulRunner.Utility;
//
// namespace SoulRunner.Player
// {
//   public class KelliActionMachine : PlayerActionMachine
//   {
//     // private IFireAction _fire;
//     // private IDashAction _dash;
//     //
//     // protected override void Awake()
//     // {
//     //   _actions
//     //     .AddItem(new PlayerMoveAction(_view))
//     //     .AddItem(new PlayerJumpAction(_view))
//     //     .AddItem(new PlayerCrouchAction(_view))
//     //     .AddItem(new PlayerLandAction(_view))
//     //     .AddItem(new KelliFireAction(_view))
//     //     .AddItem(new KelliDashAction(_view))
//     //     .AddItem(new PlayerClimbAction(_view))
//     //     .AddItem(new PlayerClimbUpAction(_view))
//     //     .AddItem(new PlayerClimbDownAction(_view))
//     //     .AddItem(new PlayerFallAction(_view))
//     //     .AddItem(new KelliSwapAction(_view));
//     //   
//     //   base.Awake();
//     //
//     //   _fire = GetAction<IFireAction>();
//     //   _dash = GetAction<IDashAction>();
//     // }
//     //
//     // protected override void ChangeHeroAction()
//     // {
//     //   _input.OnFireLeft += () => _fire.Fire(HandType.Left);
//     //   _input.OnFireRight += () => _fire.Fire(HandType.Right);
//     //   _input.OnDash += _dash.Dash;
//     // }
//     //
//     // protected override void Deactivate()
//     // {
//     //   _input.OnFireLeft -= () => _fire.Fire(HandType.Left);
//     //   _input.OnFireRight -= () => _fire.Fire(HandType.Right);
//     //   _input.OnDash -= _dash.Dash;
//     // }
//   }
// }