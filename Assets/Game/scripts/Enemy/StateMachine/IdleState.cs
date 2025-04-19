using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyBaseState
{
    private EnemyAnimator _enemyAnimator;
    private UnitAgrRadius _unitAgrRadius;
    private UnitRoadTemplate _unitRoadTemplate;
    private NavMeshAgent _agent;
    private float _timer;

    
    public IdleState(BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, UnitAgrRadius unitAgrRadius,
        UnitRoadTemplate unitRoadTemplate, NavMeshAgent meshAgent
    ) : base(baseEnemy,  enemyStateMachine)
    {
        _unitAgrRadius = unitAgrRadius;
        _unitRoadTemplate = unitRoadTemplate;
        _agent = meshAgent;
        _enemyAnimator = enemyAnimator;
    }
    
    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetIdle();
        SetDestinationToPoint();
        _agent.isStopped = false;
    }

    public override void UpdateState()
    {
        if(_unitRoadTemplate.LastPoint == null)
            return;
        
        if (_unitAgrRadius.Targets.Count > 0)
        {
            _enemyStateMachine.ChangeState<MoveState>();
        }
        else
        {
            if (Vector3.Distance(BaseEnemy.transform.position, _unitRoadTemplate.LastPoint.position) < 3f)
            {
                if(_unitRoadTemplate.TryGetPoint())
                    SetDestinationToPoint();
                else
                {
                    _enemyAnimator.SetIdle();
                }
            }
        }
    }

    private void SetDestinationToPoint()
    {
        _enemyAnimator.SetRun();
        if (NavMesh.SamplePosition(_unitRoadTemplate.LastPoint.position, out NavMeshHit hit, 4f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        } 

    }

    public override void ExitState()
    {
    }
}