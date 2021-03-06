using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    GameObject followTarget;
    float attackRange = 2f;

    int movementZhash = Animator.StringToHash("MovementZ");
    int isAttackingHash = Animator.StringToHash("isAttacking");

    private IDamagable damagebleObject;
    
    public ZombieAttackState(GameObject _followTarget,ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        updateInterval = 2f;
        
        //Set damageble object here, ADD LATER
        damagebleObject = followTarget.GetComponent<IDamagable>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        //base.Start();
        ownerZombie.zombieNavMeshAgent.isStopped = true;
        ownerZombie.zombieNavMeshAgent.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementZhash,0);
        ownerZombie.zombieAnimator.SetBool(isAttackingHash,true);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        damagebleObject?.TakeDamage(ownerZombie.zombieDamage);
        //DEAL DAMAGE EVERY INTERVAL ADD LATER
    }

    public override void Update()
    {
        //base.Update();
        ownerZombie.transform.LookAt(followTarget.transform.position,Vector3.up);
        
        
        float distanceBewtween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBewtween > attackRange)
        {
            //Change state to following here
            stateMachine.ChangeState(ZombieStateType.Following);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavMeshAgent.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, false);
    }
}
