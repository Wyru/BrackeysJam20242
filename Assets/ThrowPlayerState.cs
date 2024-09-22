using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlayerState : PlayerState
{

    public float movementSpeed = 3;
    public bool CanStart
    {
        get
        {
            return Player.playerWeapon.currentWeapon != null;
        }
    }

    public override State Next()
    {
        if (isComplete)
        {
            return Player.idlePlayerState;
        }

        return null;
    }

    public override void OnEnterState()
    {
        isComplete = false;
        Player.armsAnimator.SetTrigger("throw");
        PlayerAnimationEvents.OnAnimationFirstEvent += Throw;
        PlayerAnimationEvents.OnAnimationEndEvent += () => isComplete = true;
    }

    public override void OnExitState() { }

    public override void Run()
    {
        Player.UpdateCamera();
        Player.UpdateMovement(movementSpeed);
    }
    public override void FixedRun() { }

    void Throw()
    {
        Player.playerWeapon.Throw();
    }
}
