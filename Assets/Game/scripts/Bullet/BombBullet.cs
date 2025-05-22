using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Linq;

public class BombBullet : BaseBullet
{
    [Inject] private EnemyFactory enemyFactory;
    [Inject] private BulletFactory bulletFactory;
    public override Type Type => typeof(BombBullet);

    public override void Init(Transform target)
    {
        base.Init(target);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {

    }
    protected override void Update()
    {
        if(_target == null)
        {
            return;
        }
        var dir = _target.transform.position - transform.position;
        Debug.Log("target for bombBullet is " + _target.transform.name);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
        rigidbody.velocity = dir.normalized * WeaponUpgradeData.GetStat(StatName.ProjectileSpeed).CurrentValue;
        if(_target.transform.position.y > transform.position.y)
        {
            Explosion();
            DestroyBullet();
            //Destroy(gameObject);
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
