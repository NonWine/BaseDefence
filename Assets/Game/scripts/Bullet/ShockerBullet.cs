using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Linq;

public class ShockerBullet : BaseBullet
{
    [Inject] private EnemyFactory enemyFactory;
    public override Type Type => typeof(ShockerBullet);
    private int currentJumpCount;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //base.OnTriggerEnter2D(other);
        if(currentJumpCount < (int)WeaponUpgradeData.GetStat(StatName.ShockerJumpCount).CurrentValue)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
            {

                damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
            }
            _target = GetNearlestEnemy(transform).transform;
            if(_target = null)
            {
                DestroyBullet();
            }
            currentJumpCount++;
        }
        else
        {
            DestroyBullet();
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
