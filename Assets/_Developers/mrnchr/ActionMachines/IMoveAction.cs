using System;

namespace SoulRunner.Player.ActionMachines
{
  public interface IMoveAction
  {
    public Action<float> OnStart { get; set; }
    public void Move(float direction);
  }
}