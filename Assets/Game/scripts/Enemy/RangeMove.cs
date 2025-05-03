using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class RangeMove : IMoveable
{
    BaseEnemy _enemy;
    NavMeshAgent _navMesh;
    float _distance;
    Target _target;



    public RangeMove(BaseEnemy enemy, NavMeshAgent navMesh, float distance, Target target)
    {
        _enemy = enemy;
        _navMesh = navMesh;
        _distance = distance;
        _target = target;
    }


    public void Move()
    {
        if(Vector3.Distance(_enemy.transform.position, 
            new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, _target.transform.position.z)) <= _distance)
        {
            _enemy.EnemyStateMachine.ChangeState<AttackState>();
        }
        _navMesh.Move(new Vector3(0, 0, -1) * (_enemy.Speed * Time.deltaTime));
    }
}
