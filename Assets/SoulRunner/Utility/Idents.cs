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
      public const string Up = nameof(Up);
      public const string Down = nameof(Down);
      public const string MainAbility = nameof(MainAbility);
      public const string SideAbility = nameof(SideAbility);
      public const string PickUp = nameof(PickUp);
      public const string UseItem = nameof(UseItem);
      public const string SwapHero = nameof(SwapHero);
      public const string NextItem = nameof(NextItem);
      public const string PrevItem = nameof(PrevItem);
    }
  }
}