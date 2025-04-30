using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Archer : BaseEnemy
{
    [Inject] private BulletFactory _bulletFactory;
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private float _distance;

    protected override Dictionary<Type, IEnemyState> CreateStates()
    {
        RangeAttack rangeAttackStat = new RangeAttack(this, _bulletFactory, _bulletPos, target);
        RangeMove rangeMove = new RangeMove(this, NavMesh, _distance, target);
        var states = base.CreateStates();
        states[typeof(AttackState)] = new AttackState(this, EnemyStateMachine, rangeAttackStat, EnemyAnimator, EnemyRotation, target);
        states[typeof(MoveState)] = new MoveState(this, EnemyStateMachine, NavMesh, EnemyAnimator, EnemyRotation, target, rangeMove);
        return states;
    }

    public override Type Type => typeof(Archer);
}