using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TowerDefence : MonoBehaviour , ITickable
{
    [Inject]  private EnemyFactory enemyFactory;
    [Inject]  private BulletFactory bulletFactory;
    [Inject] private GameController _gameСontroller;
    [SerializeField] private BaseBullet baseBulletPrefab;
    [SerializeField] private Transform startBulletPos;
    [SerializeField] private float coolDown;
    [SerializeField] private int damage;
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
        
            if (timer >= coolDown)
            {
                var enemy = GetNearlestEnemy(transform);
                if (enemy != null)
                {
                    timer = 0f;
                    CurrentAgredTarget = enemy.transform;
                    var bullet = bulletFactory.Create(baseBulletPrefab.GetType());
                    bullet.transform.position = startBulletPos.position;
                    bullet.Init(damage, CurrentAgredTarget);
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
