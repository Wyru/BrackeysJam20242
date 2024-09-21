using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public override State Next()
    {
        if (Player.crouchInput.action.WasPerformedThisFrame())
        {
            return Player.crouchingPlayerState;
        }

        if (Player.action1Input.action.WasPerformedThisFrame())
        {
            return Player.attackPlayerState;
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

    public override void OnEnterState()
    {
        Player.fatigue.autoRecovery = true;
    }

    public override void OnExitState()
    {
        Player.fatigue.autoRecovery = false;

    }
    public override void Run() { }

}