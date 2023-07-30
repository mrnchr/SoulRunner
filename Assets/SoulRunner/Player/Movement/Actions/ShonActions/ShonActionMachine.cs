// using SoulRunner.Utility;
//
// namespace SoulRunner.Player
// {
//   public class ShonActionMachine : PlayerActionMachine
//   {
//     protected override void Awake()
//     {
//       _actions
//         .AddItem(new PlayerMoveAction(_view))
//         .AddItem(new ShonJumpAction(_view))
//         .AddItem(new PlayerCrouchAction(_view))
//         .AddItem(new PlayerLandAction(_view))
//         .AddItem(new PlayerClimbAction(_view))
//         .AddItem(new PlayerClimbUpAction(_view))
//         .AddItem(new PlayerClimbDownAction(_view))
//         .AddItem(new PlayerFallAction(_view))
//         .AddItem(new ShonSwapAction(_view));
//       
//       base.Awake();
//     }
//   }
// }