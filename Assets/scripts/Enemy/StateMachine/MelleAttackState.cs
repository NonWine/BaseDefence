using UnityEngine.AI;

public class EnemyMelleAttack : IAttackable
{
    private BaseEnemy _baseEnemy;

    public EnemyMelleAttack(BaseEnemy baseEnemy)
    {
        _baseEnemy = baseEnemy;
    }
    
    public void Attack(IDamageable damageable)
    {
        damageable.GetDamage(_baseEnemy.CurrentDamage);

    }
}