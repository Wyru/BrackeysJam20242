using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateMachine stateMachine;
    public bool isComplete;

    public void Setup(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void OnEnterState();
    public abstract void Run();
    public abstract void FixedRun();
    public abstract void OnExitState();

}
