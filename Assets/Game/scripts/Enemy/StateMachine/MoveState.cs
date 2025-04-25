using UnityEngine;
using UnityEngine.AI;

public class MoveState : EnemyBaseState
{
    private NavMeshAgent _agent;
    private EnemyAnimator _enemyAnimator;
    private EnemyRotation _enemyRotation;
    private float _timer;
    private Target _target;
    private Vector3 _targetDirection;
    
    public MoveState(BaseEnemy enemy, EnemyStateMachine enemyStateMachine, NavMeshAgent agent,
         EnemyAnimator enemyAnimator, EnemyRotation enemyRotation, Target target) 
        : base(enemy, enemyStateMachine)
    {
        _enemyAnimator = enemyAnimator;
        _enemyRotation = enemyRotation;
        _agent = agent;
        _target = target;
    }

    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetRun();
        _agent.isStopped = false;
        _timer = float.MaxValue;
        _targetDirection =(_target.randomPointInBoxCollider.GetRandomPointInBox() - enemy.transform.position).normalized;
    }

    public override void UpdateState()
    {
        //_agent.Move(_targetDirection * 20 * Time.deltaTime);
        _agent.Move(new Vector3(0, 0, -1) * 17 * Time.deltaTime);
    }

    public override void ExitState()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }


    
}
