using UnityEngine;

public abstract class PlayerState : State
{
    public PlayerBehavior Player
    {
        get
        {
            return stateMachine as PlayerBehavior;
        }
    }
}