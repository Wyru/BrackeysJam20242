using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public List<State> States { get; private set; }

    [Header("State Machine")]
    public State initialState;
    public State currentState;
    public State lastState;

    virtual protected void Start()
    {
        var states = GetComponentsInChildren<State>();
        States = new List<State>(states);

        foreach (var state in States)
        {
            state.Setup(this);
        }
    }

    virtual protected void Update()
    {
        if (currentState != null)
        {
            currentState.Run();
            var state = SelectNextState();
            if (state != null)
            {
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

        Debug.Log($"{name} alterando de {currentState.name} para {state.name}");

        lastState = currentState;
        currentState = state;
        currentState.OnEnterState();
    }


}
