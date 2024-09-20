using UnityEngine;

public class PlayerState : State
{
    public PlayerBehavior Player
    {
        get
        {
            return stateMachine as PlayerBehavior;
        }
    }

    public override void FixedRun()
    {
        return;
    }

    public override void OnEnterState()
    {
        return;
    }

    public override void OnExitState()
    {
        return;
    }

    public override void Run()
    {
        return;
    }
}