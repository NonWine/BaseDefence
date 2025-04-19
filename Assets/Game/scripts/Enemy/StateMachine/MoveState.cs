using UnityEngine;
using UnityEngine.AI;

public class MoveState : EnemyBaseState
{
    private NavMeshAgent _agent;
    private UnitAgrRadius _unitAgrRadius;
    private EnemyAnimator _enemyAnimator;
    private EnemyRotation _enemyRotation;
    private float _timer;
    
    public MoveState(BaseEnemy enemy, EnemyStateMachine enemyStateMachine, NavMeshAgent agent,
        UnitAgrRadius agrRadius, EnemyAnimator enemyAnimator, EnemyRotation enemyRotation) 
        : base(enemy, enemyStateMachine)
    {
        _unitAgrRadius = agrRadius;
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
        if (_unitAgrRadius.TryGetTargetDamageable(out IDamageable damageable))
        {
            UpdatePath();
            _enemyRotation.TargetRotation(_unitAgrRadius.CurrentAgredTransform);
            if (Vector3.Distance(_agent.transform.position, _unitAgrRadius.CurrentAgredTransform.position) < BaseEnemy.EnemyStatsConfig.TargetDistance)
            {
                _enemyStateMachine.ChangeState<AttackState>();
            }
        }
        else
        {
            _enemyStateMachine.ChangeState<IdleState>();
        }

    }

    public override void ExitState()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }

    private void UpdatePath()
    {
        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            _agent.SetDestination(_unitAgrRadius.CurrentAgredTransform.position);
            _timer = 0f;
            
        }
    }
    
}
