using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Linq;

public class BombBullet : BaseBullet
{
    [Inject] private GameManager gameManager;
    [Inject] private EnemyFactory enemyFactory;
    public override Type Type => typeof(BombBullet);

    public override void Init(Transform target)
    {
        base.Init(target);
        _target = gameManager.BombTarget;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {

    }
    protected override void Update()
    {
        var dir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
        rigidbody.velocity = dir.normalized * WeaponUpgradeData.GetStat(StatName.ProjectileSpeed).CurrentValue;
        if(_target.transform.position.z < transform.position.z)
        {
            Explosion();
            DestroyBullet();
        }
    }
    private void Explosion()
    {
        var activeEnemies = enemyFactory.Enemies.Where(x => x.IsDeath == false).ToList();
        foreach(var enemy in activeEnemies)
        {
            enemy.GetDamage(100000);
        }
    }

}
