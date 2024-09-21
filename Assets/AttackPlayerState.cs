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
        Player.playerWeapon.Attack();
        // PlayerAnimationEvents.OnAnimationFirstEvent += CastHitBox;
        PlayerAnimationEvents.OnAnimationEndEvent += OnAttackAnimationEnd;
    }
    public override void Run()
    {

    }
    public override void FixedRun() { }
    public override void OnExitState() { }

    public void CastHitBox()
    {
        Debug.Log("CastHitBox ");
    }

    public void OnAttackAnimationEnd()
    {
        Debug.Log("OnAttackAnimationEnd ");

        isComplete = true;
    }
}