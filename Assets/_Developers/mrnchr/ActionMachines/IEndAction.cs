using System;

namespace SoulRunner.Player.ActionMachines
{
  public interface IEndAction
  {
    public Action OnEnd { get; set; }
  }
}