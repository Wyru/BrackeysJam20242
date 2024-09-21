public class WalkingPlayerState : PlayerState
{
    public float speed = 6;
    readonly string WALKING_ANI_BOOL = "walking";


    public override State Next()
    {
        if (Player.action1Input.action.WasPerformedThisFrame())
        {
            return Player.attackPlayer;
        }

        if (Player.movement.magnitude == 0)
        {
            return Player.idlePlayerState;
        }

        return null;
    }

    public override void OnEnterState()
    {
        Player.animator.SetBool(WALKING_ANI_BOOL, true);
    }

    public override void OnExitState()
    {
        Player.animator.SetBool(WALKING_ANI_BOOL, false);
    }

    public override void Run() { }

    public override void FixedRun()
    {
        Player.UpdateCamera();
        Player.UpdateMovement(speed);
    }
}
