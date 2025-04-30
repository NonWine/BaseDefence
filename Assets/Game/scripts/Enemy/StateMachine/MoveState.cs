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
    private BaseEnemy _enemy;
    private float _speed = 17;
    private float _tempSpeed;
    private IMoveable _moveable;
    
    public MoveState(BaseEnemy enemy, EnemyStateMachine enemyStateMachine, NavMeshAgent agent,
         EnemyAnimator enemyAnimator, EnemyRotation enemyRotation, Target target, IMoveable moveable) 
        : base(enemy, enemyStateMachine)
    {
        _enemyAnimator = enemyAnimator;
        _enemyRotation = enemyRotation;
        _agent = agent;
        _target = target;
        _moveable = moveable;
        _enemy = enemy;
    }

    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetRun();
        _agent.Warp(enemy.transform.position + Vector3.up * _agent.baseOffset);
        _agent.isStopped = false;
        _timer = float.MaxValue;
        _targetDirection =(_target.randomPointInBoxCollider.GetRandomPointInBox() - enemy.transform.position).normalized;
    }
    
    public override void UpdateState()
    {
        //_agent.Move(_targetDirection * 20 * Time.deltaTime);
        //_agent.Move(new Vector3(0, 0, -1) * 17 * Time.deltaTime);
        _moveable.Move();
    }

    public override void ExitState()
    {
        if(_agent.isOnNavMesh)
            _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }


    
}
