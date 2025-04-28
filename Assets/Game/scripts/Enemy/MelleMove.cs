using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MelleMove : IMoveable
{
    BaseEnemy _enemy;
    NavMeshAgent _navMesh;

    public MelleMove(BaseEnemy enemy, NavMeshAgent naveMesh)
    {
        _enemy = enemy;
        _navMesh = naveMesh;
    }

    public void Move()
    {
        _navMesh.Move(new Vector3(0, 0, -1) * 17 * Time.deltaTime);
    }

}
