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
        _enemyAnimator.Animator.transform.localPosition = Vector3.zero;
        _enemyAnimator.Animator.transform.rotation = Quaternion.identity;
    }

    public override void UpdateState()
    {
    }



    public override void ExitState()
    {
    }
}