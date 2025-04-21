using System.Linq;
using UnityEngine;

public class PlayerGiveDamageHandler
{
    private EnemyFactory enemyFactory;
    private BulletFactory bulletFactory;
    private PlayerCombatManager playerCombatManager;
    private Player player;

    private BaseEnemy GetNearlestEnemy()
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(player.transform.position, e.transform.position))
            .FirstOrDefault();
        
        return nearestEnemy;
    }
    
    
}