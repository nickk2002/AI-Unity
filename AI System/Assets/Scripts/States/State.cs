using UnityEngine;

abstract public class State<T>
{
    public abstract void Enter(T owner);
    public abstract void Execute(T owner);
    public abstract void Exit(T owner);
    public bool inState = false;

    
}
