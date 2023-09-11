using System;
using System.Collections.Generic;

[Serializable]
public class StateMashine
{
    private AFState CurState { get; set; }

    Dictionary<Type, AFState> _states = new();

    public void AddState(AFState state) {
        _states.Add(state.GetType(), state);
    }
    public void SetState<T>() where T : AFState { 
        Type type = typeof(T);

        if (CurState != null && type == CurState.GetType()) return;

        if (_states.TryGetValue(type, out var stat))
        {
            CurState?.Exit();
            CurState = stat;
            CurState.Enter();
        }
    }

    public void Update() { 
        CurState?.Update();
    }  


}