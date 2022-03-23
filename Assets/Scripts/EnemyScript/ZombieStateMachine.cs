using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : MonoBehaviour
{
    public State currentState { get; private set; }
    protected Dictionary<ZombieStateType, State> states = new Dictionary<ZombieStateType, State>();
    private bool isRunning;

    private void Awake()
    {
    }

    public void Initialize(ZombieStateType startingState)
    {
        if (states.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
    }
    
    
    public void AddState(ZombieStateType statename, State state)
    {
        if (states.ContainsKey(statename)) return;
        states.Add(statename,state);
    }
    
    public void RemoveState(ZombieStateType statename)
    {
        if (!states.ContainsKey(statename)) return;
        states.Remove(statename);
    }
    
    public void ChangeState(ZombieStateType nextState)
    {
        if (isRunning)
        {
            StopRunningState();
        }

        if (!states.ContainsKey(nextState)) return;

        currentState = states[nextState];
        currentState.Start();

        if (currentState.updateInterval >0)
        {
            InvokeRepeating(nameof(IntervalUpdate),0, currentState.updateInterval);
        }

        isRunning = true;
    }
    
    public void StopRunningState()
    {
        isRunning = false;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if (isRunning)
        {
            currentState.IntervalUpdate();
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            currentState.Update();
        }
    }
}


