using UnityEngine;

namespace SoulRunner.Utility
{
  public static class Idents
  {
    public static class EcsWorlds
    {
      public static string message = "message";
    }

    public static class InputActions
    {
      public const string Horizontal = nameof(Horizontal);
      public const string FireLeft = nameof(FireLeft);
      public const string FireRight = nameof(FireRight);
      public const string Jump = nameof(Jump);
      public const string Crouch = nameof(Crouch);
      public const string MainAbility = nameof(MainAbility);
      public const string SideAbility = nameof(SideAbility);
      public const string PickUp = nameof(PickUp);
      public const string UseItem = nameof(UseItem);
      public const string SwapHero = nameof(SwapHero);
      public const string NextItem = nameof(NextItem);
      public const string PrevItem = nameof(PrevItem);
    }

    public static class Anims
    {
      public static readonly int IsRun = Animator.StringToHash("IsRun");
    }
  }
}