using UnityEngine;

public class CrouchingPlayerState : PlayerState
{
    public float movementSpeed = 2;
    public override State Next()
    {
        if (Player.action1Input.action.WasPressedThisFrame())
        {
            return Player.attackPlayerState;
        }

        if (isComplete)
        {
            return Player.idlePlayerState;
        }

        return null;
    }

    public override void OnEnterState()
    {
        isComplete = false;
        Player.bodyAnimator.SetBool("crouch", true);
    }

    public override void OnExitState()
    {
        Player.bodyAnimator.SetBool("crouch", false);
    }

    public override void FixedRun()
    {
        Player.UpdateCamera();
        Player.UpdateMovement(movementSpeed);
    }

    public override void Run()
    {
        if (Player.crouchInput.action.WasPressedThisFrame())
        {
            isComplete = true;
        }
    }
}
