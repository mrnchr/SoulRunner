namespace SoulRunner.Fireball
{
  public class FireballAliveState : FireballState
  {
    public override bool CanStart() => true;

    public override void Start()
    {
    }

    public override void End()
    {
    }

    public override void Update()
    {
      if (Variables.IsCollided)
        Machine.ChangeState(FireballStateType.Dead);
    }
  }
}