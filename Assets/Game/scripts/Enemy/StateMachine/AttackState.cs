using UnityEngine;

public  class AttackState : EnemyBaseState
{
    private IAttackable _attackable;
    private EnemyAnimator _enemyAnimator;
    private EnemyRotation _enemyRotation;
    private Target _target;
    
    public AttackState(BaseEnemy baseEnemy, EnemyStateMachine enemyStateMachine, IAttackable attackable, 
        EnemyAnimator enemyAnimator, EnemyRotation enemyRotation, Target target) : base(baseEnemy, enemyStateMachine)
    {
        _attackable = attackable;
        _enemyAnimator = enemyAnimator;
        _enemyRotation = enemyRotation;
        _target = target;
    }

    
    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetAttack();
        _enemyAnimator.AnimationEvent.OnAttack += Attack;
    }

    public override void UpdateState()
    {
       
    }


    public override void ExitState()
    {
        _enemyAnimator.AnimationEvent.OnAttack -= Attack;
    }
    
    

    private void Attack()
    {
        if(!_target || _target.IsDeath)
        {
            return;
        }
        _target.GetDamage(10);
    }
}