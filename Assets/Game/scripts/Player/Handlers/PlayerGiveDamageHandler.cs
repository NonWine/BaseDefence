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
            if (upgradeData.IsHaveStat(StatName.ProjectileCountPerTime))
            {
                ShootPerCountTime(player, unlockedWeapon, upgradeData);
            }
            else
            {
                ShootToEnemy(player, unlockedWeapon);
            }
            
        }
    }

    private void ShootPerCountTime(Player player, DynamicWeaponHandler unlockedWeapon, WeaponUpgradeData upgradeData)
    {
        if (unlockedWeapon.CurrentTimer >= upgradeData.GetStat(StatName.CoolDown).CurrentValue)
        {
            for (int i = 0; i < upgradeData.GetStat(StatName.ProjectileCountPerTime).CurrentValueInt; i++)
            {
                ShootToEnemy(player, unlockedWeapon, i /2f);
            }

        }
    }

    private void ShootToEnemy(Player player, DynamicWeaponHandler unlockedWeapon, float offset = 0f)
    {
        var enemy = GetNearlestEnemy(player.transform);
        if (enemy != null)
        {
            CurrentAgredTarget = enemy.transform;
            unlockedWeapon.CurrentTimer = 0f;
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