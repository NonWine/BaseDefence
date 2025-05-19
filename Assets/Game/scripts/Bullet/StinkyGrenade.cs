using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StinkyGrenade : BaseBullet
{
    [SerializeField] private LayerMask damageableMask;
    public override Type Type => typeof(StinkyGrenade);
    private Collider2D[] _overlapResults = new Collider2D[20];
    [SerializeField] private StinkyCloud stinkyCloud;
    private float radiusExplose => WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            Debug.Log("radius is " + WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue);
            var cloud = Instantiate(stinkyCloud, transform.position, Quaternion.identity);
            cloud.GetComponent<StinkyCloud>().Init(WeaponUpgradeData);
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, radiusExplose, _overlapResults, damageableMask);
            for (int i = 0; i < count; i++)
            {
                if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
                {
                    targetDamageable.GetDamage(WeaponUpgradeData.GetStat(StatName.ExplosiveDamage).CurrentValueInt);
                }
            }
            DestroyBullet();
        }

    }
    protected override void DestroyBullet()
    {
        ParticlePool.Instance.PlayExplossion(transform.position, radiusExplose);
        base.DestroyBullet();
    }
}
