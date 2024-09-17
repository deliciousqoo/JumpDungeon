using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine
{
    private Character _ownerEntity;
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }
    

    public void Setup(Character owner, State entryState)
    {
        _ownerEntity = owner;
        CurrentState = null;

        ChangeState(entryState);
    }

    public void Execute()
    {
        if(CurrentState != null)
        {
            CurrentState.Execute(_ownerEntity);
        }
    }

    public void ChangeState(State newState)
    {
        if (newState == null) return;

        if(CurrentState != null)
        {
            PreviousState = CurrentState;

            CurrentState.Exit(_ownerEntity);
        }

        CurrentState = newState;
        CurrentState.Enter(_ownerEntity);
    }

    public void RevertToPreviousState()
    {
        ChangeState(PreviousState);
    }
}
