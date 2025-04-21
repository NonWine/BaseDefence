using UnityEngine;

public  class AttackState : EnemyBaseState
{
    private IAttackable _attackable;
    private EnemyAnimator _enemyAnimator;
    private EnemyRotation _enemyRotation;
    
    public AttackState(BaseEnemy baseEnemy, EnemyStateMachine enemyStateMachine, IAttackable attackable, 
        EnemyAnimator enemyAnimator, EnemyRotation enemyRotation) : base(baseEnemy, enemyStateMachine)
    {
        _attackable = attackable;
        _enemyAnimator = enemyAnimator;
        _enemyRotation = enemyRotation;
        
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
    }
}