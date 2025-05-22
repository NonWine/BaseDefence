using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class ResetingState : EnemyBaseState
{
    private HealthUI _healthUI;
    private EnemyAnimator enemyAnimator;
    
    public ResetingState( BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine,
        HealthUI healthUI, EnemyAnimator enemyAnimator) : base(baseEnemy,  enemyStateMachine)
    {
        _healthUI = healthUI;
        this.enemyAnimator = enemyAnimator;
    }    
    
    public override void EnterState(BaseEnemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemyAnimator.Animator.enabled = false;
        enemyAnimator.Animator.enabled = true;
        enemy.transform.localScale = Vector3.zero;
        enemy.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);
        enemy.CurrentHealth = enemy.MaxHealth;
        enemy.Speed = enemy.EnemyStatsConfig._moveSpeed;

        enemy.CurrentDamage = enemy.EnemyStatsConfig.Damage;
        _healthUI.gameObject.SetActive(true);
        _healthUI.SetHealth(enemy.MaxHealth);
        enemy.UnsetDeath();
        _enemyStateMachine.ChangeState<IdleState>();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }
}