using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyBaseState
{
    private EnemyAnimator _enemyAnimator;
    private NavMeshAgent _agent;
    private float _timer;

    
    public IdleState(BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator,
         NavMeshAgent meshAgent
    ) : base(baseEnemy,  enemyStateMachine)
    {
        _agent = meshAgent;
        _enemyAnimator = enemyAnimator;
    }
    
    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.Animator.applyRootMotion = false;
        _enemyAnimator.SetIdle();
        _agent.enabled = false;
        _agent.enabled = true;
        _agent.isStopped = false;
    }

    public override void UpdateState()
    {
    }



    public override void ExitState()
    {
    }
}