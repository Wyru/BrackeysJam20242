using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public override State Next()
    {

        if (Player.action1Input.action.WasPerformedThisFrame())
        {
            return Player.attackPlayer;
        }

        if (Player.movement.magnitude > 0)
        {
            return Player.walkingPlayerState;
        }

        return null;
    }

    public override void FixedRun()
    {
        Player.UpdateCamera();
    }

    public override void OnEnterState() { }
    public override void OnExitState() { }
    public override void Run() { }

}