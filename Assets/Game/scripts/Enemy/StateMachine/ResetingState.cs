using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class ResetingState : EnemyBaseState
{
    private HealthUI _healthUI;
    private NavMeshAgent navMeshAgent;
    private EnemyAnimator enemyAnimator;
    
    public ResetingState( BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine,
        HealthUI healthUI, NavMeshAgent navMeshAgent, EnemyAnimator enemyAnimator) : base(baseEnemy,  enemyStateMachine)
    {
        _healthUI = healthUI;
        this.enemyAnimator = enemyAnimator;
        this.navMeshAgent = navMeshAgent;
    }    
    
    public override void EnterState(BaseEnemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemyAnimator.Animator.enabled = false;
        enemyAnimator.Animator.enabled = true;
        navMeshAgent.enabled = true;
        enemy.transform.localScale = Vector3.zero;
        enemy.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);
        enemy.CurrentHealth = enemy.EnemyStatsConfig.MaxHealth;
        enemy.CurrentDamage = enemy.EnemyStatsConfig.Damage;
        _healthUI.gameObject.SetActive(true);
        _healthUI.SetHealth(enemy.EnemyStatsConfig.MaxHealth);
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