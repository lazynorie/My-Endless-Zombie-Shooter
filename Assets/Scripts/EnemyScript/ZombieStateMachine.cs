using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : MonoBehaviour
{
    public State currenntState { get; private set; }

    protected Dictionary<ZombieStateType, State> state;

    private bool isRunning;
 
    // Update is called once per frame
    void Update()
    {
        
    }
}


