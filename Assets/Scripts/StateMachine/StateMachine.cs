using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public List<State> States { get; private set; }


    public State initialState;
    public State currentState;
    public State lastState;

    virtual protected void Start()
    {
        var states = GetComponentInChildren<State>();
        States = new List<State> { states };
    }

    virtual protected void Update()
    {
        if (currentState != null)
        {
            currentState.Run();
            if (currentState.isComplete)
            {
                var state = SelectNextState();
                ChangeState(state);
            }
        }
    }

    virtual protected void FixedUpdate()
    {
        if (currentState != null)
            currentState.FixedRun();
    }

    virtual protected State SelectNextState()
    {
        return null;
    }

    virtual protected void ChangeState(State state)
    {
        if (state == null)
            return;

        if (currentState != null)
            currentState.OnExitState();

        lastState = currentState;
        currentState = state;
        currentState.OnEnterState();
    }


}
