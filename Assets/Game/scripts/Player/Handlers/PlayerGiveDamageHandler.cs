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
            var upgradeData = unlockedWeapon.weaponInfoData.WeaponUpgradeData;
            if (unlockedWeapon.CurrentTimer >= upgradeData.GetStat(StatName.CoolDown).CurrentValue)
            {
                ShootPerCountTime(player, unlockedWeapon, upgradeData);
            }
            
        }
    }

    private void ShootPerCountTime(Player player, DynamicWeaponHandler unlockedWeapon, WeaponUpgradeData upgradeData)
    {
        if (upgradeData.IsHaveStat(StatName.ProjectileCountPerTime))
        {
            // unlockedWeapon.perShootTimer = 1f;
            for (int j = 0; j < upgradeData.GetStat(StatName.ProjectileCountPerTime).CurrentValueInt; j++)
            {
                ShootToEnemy(player, unlockedWeapon, j / 2f);
            }

            if (unlockedWeapon.CurrentShoot >= upgradeData.GetStat(StatName.ShootCountPerTime).CurrentValueInt)
            {
                unlockedWeapon.CurrentTimer = 0f;
                unlockedWeapon.CurrentShoot = 0;

            }
            else
            {
                unlockedWeapon.CurrentTimer -= 0.15f;
                unlockedWeapon.CurrentShoot++;
            }
            
        }
        else
        {
            ShootToEnemy(player, unlockedWeapon);
            unlockedWeapon.CurrentTimer = 0f;
        }
    }

    private void ShootToEnemy(Player player, DynamicWeaponHandler unlockedWeapon, float offset = 0f)
    {
        var enemy = GetNearlestEnemy(player.transform);
        if (enemy != null)
        {
            CurrentAgredTarget = enemy.transform;
            var bullet = bulletFactory.Create(unlockedWeapon.weaponInfoData.baseBullet.GetType());
            bullet.transform.position = (Vector2)player.bulletStartPoint.position + (Vector2.up* offset);
            bullet.Init(CurrentAgredTarget);
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