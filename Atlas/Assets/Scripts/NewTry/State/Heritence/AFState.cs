using System.Collections;
using UnityEngine;

public abstract class AFState 
{
    protected readonly StateMashine rootMashine;

    public AFState (StateMashine rootMashine)
    {
        this.rootMashine = rootMashine;
    }
    public virtual void Update(){}
    public virtual void Exit() {}
    public virtual void Enter(){ }

}
