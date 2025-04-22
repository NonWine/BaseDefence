using UnityEngine;
using UnityEngine.AI;

public class MoveState : EnemyBaseState
{
    private NavMeshAgent _agent;
    private EnemyAnimator _enemyAnimator;
    private EnemyRotation _enemyRotation;
    private float _timer;
    
    public MoveState(BaseEnemy enemy, EnemyStateMachine enemyStateMachine, NavMeshAgent agent,
         EnemyAnimator enemyAnimator, EnemyRotation enemyRotation) 
        : base(enemy, enemyStateMachine)
    {
        _enemyAnimator = enemyAnimator;
        _enemyRotation = enemyRotation;
        _agent = agent;
    }

    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetRun();
        _agent.isStopped = false;
        _timer = float.MaxValue;
    }

    public override void UpdateState()
    {

        _agent.Move(new Vector3(0, 0, -1) * 10 * Time.deltaTime);
    }

    public override void ExitState()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }


    
}
