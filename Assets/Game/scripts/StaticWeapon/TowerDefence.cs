using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TowerDefence : StaticWeaponObj , ITickable
{
    [Inject]  private EnemyFactory enemyFactory;
    [Inject]  private BulletFactory bulletFactory;
    [Inject] private GameController _gameСontroller;
    [SerializeField] private BaseBullet baseBulletPrefab;
    [SerializeField] private Transform startBulletPos;

    public Transform CurrentAgredTarget { get; private set; }
    
    private float timer;

    private void Awake()
    {
        _gameСontroller.RegisterInTick(this);
    }
    
    public void Tick()
    {
        TryGetDamage();
    }

    
    public void TryGetDamage()
    {
        timer += Time.deltaTime;
        
            if (timer >= WeaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue)
            {
                var enemy = GetNearlestEnemy(transform);
                if (enemy != null)
                {
                    timer = 0f;
                    CurrentAgredTarget = enemy.transform;
                    var bullet = bulletFactory.Create(baseBulletPrefab.GetType());
                    bullet.transform.position = startBulletPos.position;
                    bullet.Init(CurrentAgredTarget);
                }
                
            }
    }

    private BaseEnemy GetNearlestEnemy(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .Where(e => !e.IsDeath && Vector3.Distance(thisTarget.position, e.transform.position) < 25)
            .OrderBy(e => Vector3.Distance(thisTarget.position, e.transform.position))
            .FirstOrDefault();

        return nearestEnemy;
    }
    
}
