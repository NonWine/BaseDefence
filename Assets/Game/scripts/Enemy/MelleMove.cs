using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MelleMove : IMoveable
{
    BaseEnemy _enemy;
    Rigidbody2D _rigidbody;

    public MelleMove(BaseEnemy enemy, Rigidbody2D rigidbody)
    {
        _enemy = enemy;
        _rigidbody = rigidbody;
    }

    public void Move()
    {
        //_navMesh.Move(new Vector3(0, 0, -1) * (_enemy.Speed * Time.deltaTime));
        //_rigidbody.MovePosition(_enemy.transform.position + new Vector3(0, -1, 0) * _enemy.Speed * Time.deltaTime);
        _rigidbody.velocity = new Vector3(0, -1, 0) * _enemy.Speed * Time.deltaTime;
    }

}
