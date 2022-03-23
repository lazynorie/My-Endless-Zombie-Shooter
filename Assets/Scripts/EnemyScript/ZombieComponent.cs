using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    public int zombieDamage = 5;

    public NavMeshAgent zombieNavMeshAgent;
    public Animator zombieAnimator;
    public ZombieStateMachine zombieStateMachine;
    public GameObject followTarget;

    private void Awake()
    {
        zombieNavMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public void Initialize(GameObject _followTarget)
    {
        followTarget = _followTarget;
    }
}
