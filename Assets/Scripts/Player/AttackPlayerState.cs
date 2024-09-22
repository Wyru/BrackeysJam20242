using UnityEngine;

public class AttackPlayerState : PlayerState
{
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

        if (Player.lastState == Player.walkingPlayerState)
        {
            Player.armsAnimator.SetTrigger("wasWalking");
        }

        Player.playerWeapon.Attack();

        // PlayerAnimationEvents.OnAnimationFirstEvent += CastHitBox;
        PlayerAnimationEvents.OnAnimationEndEvent += OnAttackAnimationEnd;


    }
    public override void Run()
    {

    }
    public override void FixedRun() { }
    public override void OnExitState() { }


    public void OnAttackAnimationEnd()
    {
        Debug.Log("OnAttackAnimationEnd ");

        isComplete = true;
    }
}