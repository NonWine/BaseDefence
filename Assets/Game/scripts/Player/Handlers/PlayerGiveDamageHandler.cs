using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using Zenject;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(100)]
public class PlayerGiveDamageHandler : MonoBehaviour
{
     [Inject]  private EnemyFactory enemyFactory;
     [Inject]  private BulletFactory bulletFactory;
     [SerializeField] private PlayerCombatManager playerCombatManager;
     [SerializeField] private StaticWeaponData staticWeaponDataAmmo;
     [SerializeField] private PlayerContainer _playerContainer;
    [Inject] PlayerHandler playerHandler;
     private float timer;

     private void OnEnable()
     {
         WaveManager.Instance.OnStartWave += ResetFirstShot;
     }

     private void OnDisable()
     {
         WaveManager.Instance.OnStartWave -= ResetFirstShot;
     }

     public Transform CurrentAgredTarget { get;  set; }

     public void TryToShootByTimer(Player player)
     {
         foreach (var unlockedWeapon in playerCombatManager.UnlockedWeapons)
         {
             if(unlockedWeapon.weaponInfoData.WeaponUpgradeData.IsUnLocked == false)
                 return;
             var upgradeData = unlockedWeapon.weaponInfoData.WeaponUpgradeData;
             if (!unlockedWeapon.ShootOnce)
             {
                 unlockedWeapon.ShootOnce = true;
                 ShootPerCountTime(player, unlockedWeapon, upgradeData);
             }
             unlockedWeapon.CurrentTimer += Time.deltaTime;
             if (unlockedWeapon.CurrentTimer >= upgradeData.GetStat(StatName.CoolDown).CurrentValue)
             {
                 //ShootPerCountTime(player, unlockedWeapon, upgradeData);
                 unlockedWeapon.ShootOnce = false;
             }
            
         }
     }

     private void CountTimerForWeapons(Player player)
     {
         foreach (var unlockedWeapon in playerCombatManager.UnlockedWeapons)
         {
             if(unlockedWeapon.weaponInfoData.WeaponUpgradeData.IsUnLocked == false)
                 return;
             var upgradeData = unlockedWeapon.weaponInfoData.WeaponUpgradeData;
             if (unlockedWeapon.CurrentTimer >= upgradeData.GetStat(StatName.CoolDown).CurrentValue)
             {
                 //ShootPerCountTime(player, unlockedWeapon, upgradeData);
                 unlockedWeapon.ShootOnce = false;
                 return;
             }
             unlockedWeapon.CurrentTimer += Time.deltaTime;
            
         }
     }
    
    public void TryGetDamage(Player player)
    {   
        timer += Time.deltaTime;
        //Debug.Log(playerCombatManager);
        var enemy = GetNearestEnemy(player.transform,playerCombatManager.DistanceToAgr);

        /*foreach (var unlockedWeapon in playerCombatManager.UnlockedWeapons)
        {
            if(unlockedWeapon.weaponInfoData.WeaponUpgradeData.IsUnLocked == false)
                return;
            var upgradeData = unlockedWeapon.weaponInfoData.WeaponUpgradeData;
            if (!unlockedWeapon.ShootOnce)
            {
                unlockedWeapon.ShootOnce = true;
                ShootPerCountTime(player, unlockedWeapon, upgradeData);
            }
            unlockedWeapon.CurrentTimer += Time.deltaTime;
            if (unlockedWeapon.CurrentTimer >= upgradeData.GetStat(StatName.CoolDown).CurrentValue)
            {
                //ShootPerCountTime(player, unlockedWeapon, upgradeData);
                unlockedWeapon.ShootOnce = false;
            }
            
        }*/
        
        if (enemy == null)
        {
            CountTimerForWeapons(player);
            if (playerHandler.Player.PlayerStateMachine.CurrentStateKey != PlayerStateKey.Idle)
            {
                playerHandler.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Idle);
            }
        }
        else
        {
            TryToShootByTimer(player);
        }
        
    }

    public void ResetFirstShot()
    {
        Debug.Log("reset first shoot");
        foreach (var unlockedWeapon in playerCombatManager.UnlockedWeapons)
        {
            unlockedWeapon.ShootOnce = false;
        }
    }

    private void ShootPerCountTime(Player player, DynamicWeaponHandler unlockedWeapon, WeaponUpgradeData upgradeData)
    {
        int additionalAmmo = 0;
        if (staticWeaponDataAmmo.WeaponUpgradeData.IsUnLocked)
            additionalAmmo = staticWeaponDataAmmo.WeaponUpgradeData.GetStat(StatName.ShootCountPerTime)
                .CurrentValueInt;
        
        if (upgradeData.IsHaveStat(StatName.ProjectileCountPerTime))
        {
            // unlockedWeapon.perShootTimer = 1f;
            for (int j = 0; j < upgradeData.GetStat(StatName.ProjectileCountPerTime).CurrentValueInt; j++)
            {
                ShootToEnemy(player, unlockedWeapon, j);
            }
            unlockedWeapon.CurrentShoot++;

            if (unlockedWeapon.CurrentShoot >= upgradeData.GetStat(StatName.ShootCountPerTime).CurrentValueInt + additionalAmmo)
            {
                unlockedWeapon.CurrentTimer = 0f;
                unlockedWeapon.CurrentShoot = 0;

            }
            else
            {
                unlockedWeapon.CurrentTimer -= 0.15f;
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
        
        
        var enemy = GetNearestEnemy(player.transform,playerCombatManager.DistanceToAgr);
        if (enemy != null)
        {
            if (playerHandler.Player.PlayerStateMachine.CurrentStateKey != PlayerStateKey.Attack)
            {
                playerHandler.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
            }
            CurrentAgredTarget = enemy.transform;
            var bullet = bulletFactory.Create(unlockedWeapon.weaponInfoData.baseBullet.GetType());
            //bullet.transform.SetParent(player.bulletStartPoint);
            bullet.transform.position = (Vector2)player.bulletStartPoint.position + (Vector2.up * offset) +
                                        (Vector2.right * Random.Range(-offset, offset));
            
            Vector3 direction = CurrentAgredTarget.position - transform.position;
            /*            Quaternion LookDirection = Quaternion.LookRotation(direction);
                        transform.rotation = LookDirection;*/
            _playerContainer.Direction = direction;
            bullet.Init(CurrentAgredTarget);
        }

    }

    public BaseEnemy GetNearestEnemy(Transform thisTarget, float maxDistance)
    {
        var nearestEnemy = enemyFactory.Enemies
            .Where(e => !e.IsDeath && Vector3.Distance(thisTarget.position, e.transform.position) < maxDistance)
            .OrderBy(e => Vector3.Distance(thisTarget.position, e.transform.position))
            .FirstOrDefault();

        return nearestEnemy;
    }

    
    
}