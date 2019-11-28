using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<entityType>
{
    State<entityType> curentState;
    State<entityType> previousState;
    State<entityType> globalState;
    entityType owner;

    public StateMachine(entityType entityOwner)
    {
        owner = entityOwner;
        previousState = null;
        curentState = null;
        globalState = null;
    }
    public void SetCurentState(State<entityType> s) { curentState = s; curentState.Enter(owner); }
    public void SetPreviousState(State<entityType> s) {  previousState = s; previousState.Enter(owner); }
    public void SetGlobalState(State<entityType> s) { globalState = s; globalState.Enter(owner); }

    public void Update()
    {
        if (globalState != null)
            globalState.Execute(owner);
        if (curentState != null)
            curentState.Execute(owner);
    }
    public void ChangeState(State<entityType>newState)
    {
        previousState = curentState;
        curentState.Exit(owner);
        curentState = newState;
        curentState.Enter(owner);
    }


    public void RevertPreviousState()
    {
        ChangeState(previousState);
    }
    public bool isInState(State<entityType> state)
    {
        return state.inState;
    }

}
