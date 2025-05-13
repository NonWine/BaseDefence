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
    [SerializeField] private LayerMask _enemyLayer;
    public override void Init(Transform target)
    {
        base.Init(target);
        currentJumpCount = 0;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //base.OnTriggerEnter2D(other);
        if(currentJumpCount < (int)WeaponUpgradeData.GetStat(StatName.ShockerJumpCount).CurrentValue)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
            {

                damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
            }
            _target = FindNearestEnemyInRadius();
            if (_target == null)
            {
                Debug.Log("target after seeking new target is null");
                DestroyBullet();
                return;
            }
            _damageable = _target.GetComponent<IDamageable>();
            currentJumpCount++;
        }
        else
        {
            Debug.Log("shocker jumped maximum jumps");
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
    private Transform FindNearestEnemyInRadius()
    {
        // Get all enemies in radius
        float _searchRadius = WeaponUpgradeData.GetStat(StatName.ShockerJumpRadius).CurrentValue;
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _searchRadius,
            _enemyLayer
        );

        // Filter and find nearest
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            // Skip if not damageable or is dead
            if (!hit.transform.TryGetComponent<IDamageable>(out var enemy) || enemy.IsDeath || hit.transform == _target)
                continue;

            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

}
