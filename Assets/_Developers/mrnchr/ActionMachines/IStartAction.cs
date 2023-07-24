using System;

namespace SoulRunner.Player.ActionMachines
{
  public interface IStartAction
  {
    public Action OnStart { get; set; }
  }
}