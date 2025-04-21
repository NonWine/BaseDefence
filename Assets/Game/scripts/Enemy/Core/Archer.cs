using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Archer : BaseEnemy
{
    [Inject] private BulletFactory _bulletFactory;
    [SerializeField] private Transform _bulletPos;

    protected override Dictionary<Type, IEnemyState> CreateStates()
    {
        RangeAttack rangeAttackStat = new RangeAttack(this, _bulletFactory, _bulletPos);
        var states = base.CreateStates();
        states[typeof(AttackState)] = new AttackState(this, EnemyStateMachine, rangeAttackStat, EnemyAnimator, EnemyRotation);
        return states;
    }

    public override Type Type => typeof(Archer);
}