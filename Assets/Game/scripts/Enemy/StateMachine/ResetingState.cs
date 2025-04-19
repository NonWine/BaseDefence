using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class ResetingState : EnemyBaseState
{
    private HealthUI _healthUI;
    
    public ResetingState( BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine,
        HealthUI healthUI) : base(baseEnemy,  enemyStateMachine)
    {
        _healthUI = healthUI;
    }    
    
    public override void EnterState(BaseEnemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.localScale = Vector3.zero;
        enemy.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);
        enemy.CurrentHealth = enemy.EnemyStatsConfig.MaxHealth;
        enemy.CurrentDamage = enemy.EnemyStatsConfig.Damage;
        _healthUI.gameObject.SetActive(true);
        _healthUI.SetHealth(enemy.EnemyStatsConfig.MaxHealth);
        enemy.UnsetDeath();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }
}