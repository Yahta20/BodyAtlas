using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteteBeh : MonoBehaviour
{
    private Dictionary<Type, IState> statemap;
    private IState curState;
    public scrollBeh scroll;



    void Start()
    {
        InitStates();
        DEfState();
    }

    private void DEfState()
    {
        SetInformation();
    }
    private IState GetState<T>() where T : IState { 
        var tupe =typeof(T);
        return statemap[tupe ];
    }

    private void SetState(IState newstate) {
        if (curState != null) { 
            curState.Exit();
        }
        curState = newstate;
        curState.Enter();
    }
        
    private void InitStates()
    {
        statemap = new Dictionary<Type, IState>();

        statemap[typeof(Testing)] = new Testing(scroll);
        statemap[typeof(Information)] = new Information();

    }

    public void SetInformation() {
        SetState(GetState<Information>());
    }
    public void SetTesting() {
        SetState(GetState<Testing>());
    }

    // Update is called once per frame
    void Update()
    {
        if (curState!=null)
        {
            curState.Update();
        }
    }
}
