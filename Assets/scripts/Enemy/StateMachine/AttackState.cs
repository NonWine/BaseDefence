using UnityEngine;

public  class AttackState : EnemyBaseState
{
    private IAttackable _attackable;
    private EnemyAnimator _enemyAnimator;
    private UnitAgrRadius _unitAgrRadius;
    private EnemyRotation _enemyRotation;
    public AttackState(BaseEnemy baseEnemy, EnemyStateMachine enemyStateMachine, IAttackable attackable, 
        EnemyAnimator enemyAnimator, UnitAgrRadius unitAgrRadius, EnemyRotation enemyRotation) : base(baseEnemy, enemyStateMachine)
    {
        _attackable = attackable;
        _enemyAnimator = enemyAnimator;
        _unitAgrRadius = unitAgrRadius;
        _enemyRotation = enemyRotation;
        
    }

    
    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetAttack();
        _enemyAnimator.AnimationEvent.OnAttack += Attack;
    }

    public override void UpdateState()
    {
        if (_unitAgrRadius.TryGetTargetDamageable(out IDamageable damageable))
        {
            _enemyRotation.TargetRotation(_unitAgrRadius.CurrentAgredTransform);
            
            if (damageable is IUnitDamagable unitDamagable)
            {
               if(Vector3.Distance(BaseEnemy.transform.position, _unitAgrRadius.CurrentAgredTransform.position) > BaseEnemy.EnemyStatsConfig.TargetDistance)
                    _enemyStateMachine.ChangeState<MoveState>(); 
            }

            if(damageable.IsDeath)
                _enemyStateMachine.ChangeState<IdleState>();
        }
        else
            _enemyStateMachine.ChangeState<IdleState>();

    }


    public override void ExitState()
    {
        _enemyAnimator.AnimationEvent.OnAttack -= Attack;
    }
    
    

    private void Attack()
    {
        if (_unitAgrRadius.TryGetTargetDamageable(out IDamageable damageable))
            _attackable.Attack(damageable);
    }
}