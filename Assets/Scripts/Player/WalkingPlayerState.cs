using UnityEngine;

public class WalkingPlayerState : PlayerState
{
    public float speed = 6;
    public float runningSpeed = 12;
    public float fatigueSpeed = 3;
    public float staminaDropRate = 2;
    readonly string WALKING_ANI_BOOL = "walking";
    readonly string RUNNING_ANI_BOOL = "running";
    bool isRunning;

    public FootstepController footstep;

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

        if (Player.movement.magnitude == 0)
        {
            return Player.idlePlayerState;
        }

        return null;
    }

    public override void OnEnterState()
    {
        footstep.isWalking = true;
        isRunning = false;
        Player.armsAnimator.SetBool(WALKING_ANI_BOOL, true);
    }

    public override void OnExitState()
    {
        footstep.isWalking = false;
        footstep.isRunning = false;
        footstep.isFatigue = false;

        isRunning = false;
        Player.armsAnimator.SetBool(WALKING_ANI_BOOL, false);
    }

    public override void Run()
    {
        bool input = Player.runInput.action.IsPressed();

        if (input && !isRunning)
        {
            // on start running

        }

        isRunning = input;

        footstep.isRunning = isRunning;
        footstep.isFatigue = Player.fatigue.isFatigued;
        Player.fatigue.autoRecovery = !isRunning;

        if (isRunning && !Player.fatigue.isFatigued)
        {
            Player.armsAnimator.SetBool(RUNNING_ANI_BOOL, true);
            Player.fatigue.ChangeStamina(-staminaDropRate * Time.deltaTime);
        }
        else
        {
            Player.armsAnimator.SetBool(RUNNING_ANI_BOOL, false);

        }
    }

    public override void FixedRun()
    {
        Player.UpdateCamera();
        Player.UpdateMovement(GetSpeed());
    }

    float GetSpeed()
    {
        if (Player.fatigue.isFatigued)
            return fatigueSpeed;

        if (isRunning)
            return runningSpeed;

        return speed;
    }
}
