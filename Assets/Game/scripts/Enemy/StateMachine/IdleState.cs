using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyBaseState
{
    private EnemyAnimator _enemyAnimator;
    private NavMeshAgent _agent;
    private float _timer;

    
    public IdleState(BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator        
    ) : base(baseEnemy,  enemyStateMachine)
    {
       
        _enemyAnimator = enemyAnimator;
    }
    
    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.Animator.applyRootMotion = false;
        
        _enemyAnimator.SetIdle();
        _enemyAnimator.Animator.transform.rotation = Quaternion.identity;
        enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        ToMoveState();
    }

    public override void UpdateState()
    {
    }



    public override void ExitState()
    {
    }

    private async void ToMoveState()
    {
        await UniTask.DelayFrame(5);
        _enemyStateMachine.ChangeState<MoveState>();
    }
}