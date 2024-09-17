using UnityEngine;

public abstract class State
{ 
    public abstract void Enter(Character entity);
    public abstract void Execute(Character entity);
    public abstract void Exit(Character entity);
}
