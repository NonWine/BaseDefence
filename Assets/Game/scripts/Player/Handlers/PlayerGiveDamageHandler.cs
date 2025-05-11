using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerGiveDamageHandler
{
  [Inject]  private EnemyFactory enemyFactory;
  [Inject]  private BulletFactory bulletFactory;
  [Inject]  private PlayerCombatManager playerCombatManager;
    private float timer;
    
    
    public Transform CurrentAgredTarget { get; private set; }
    
    public void TryGetDamage(Player player)
    {
        timer += Time.deltaTime;
        foreach (var unlockedWeapon in playerCombatManager.UnlockedWeapons)
        {
            unlockedWeapon.CurrentTimer += Time.deltaTime;
            if (unlockedWeapon.CurrentTimer >= unlockedWeapon.weaponInfoData.WeaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue)
            {
                var enemy = GetNearlestEnemy(player.transform);
                if (enemy != null)
                {
                    CurrentAgredTarget = enemy.transform;
                    unlockedWeapon.CurrentTimer = 0f;
                    var bullet = bulletFactory.Create(unlockedWeapon.weaponInfoData.baseBullet.GetType());
                    bullet.transform.position = player.bulletStartPoint.position;
                    bullet.Init(CurrentAgredTarget);
                }
                
            }
            
        }
    }

    private BaseEnemy GetNearlestEnemy(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(thisTarget.transform.position, e.transform.position))
            .FirstOrDefault(x => x.IsDeath == false);
        
        
        return nearestEnemy;
    }
    
    
}