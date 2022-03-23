using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStates : State
{
    protected ZombieComponent ownerZombie;

    public ZombieStates(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(stateMachine)
    {
        ownerZombie = zombie;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum ZombieStateType
{
    Idling,
    Attacking,
    Following,
    Dying
}
