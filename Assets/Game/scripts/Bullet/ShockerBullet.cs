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
        Physics2D.queriesHitTriggers = true;
    }
    private BaseEnemy GetNearlestEnemy(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(thisTarget.transform.position, e.transform.position))
            .FirstOrDefault(x => x.IsDeath == false);


        return nearestEnemy;
    }
    protected override void Update()
    {
        var dir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
        rigidbody.velocity = dir.normalized * WeaponUpgradeData.GetStat(StatName.ProjectileSpeed).CurrentValue;
        savedDirection = dir;
        if (Vector3.Distance(_target.transform.position, transform.position) < 0.6f)
        {
            _target.GetComponent<IDamageable>().GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
            _target = FindNearestEnemyInRadius();
            if (_target == null)
            {
                DestroyBullet();
                return;
            }
            _damageable = _target.GetComponent<IDamageable>();
            currentJumpCount++;
            if (currentJumpCount >= (int)WeaponUpgradeData.GetStat(StatName.ShockerJumpCount).CurrentValue)
            {
                DestroyBullet();
            }
        }
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