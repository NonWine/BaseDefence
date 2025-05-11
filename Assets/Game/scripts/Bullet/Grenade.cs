using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grenade : BaseBullet
{
    [SerializeField] private float torqueForce = 10f;
    [SerializeField] private LayerMask damageableMask;
    private Collider2D[] _overlapResults = new Collider2D[20];

    private float radiusExplose => WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue;
    
    public override Type Type => typeof(Grenade);

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, radiusExplose, _overlapResults,damageableMask);
            for (int i = 0; i < count; i++)
            {
                if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
                {
                    targetDamageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
                }
            }
            DestroyBullet();
        }
        
    }
    
    

    public override void Init(Transform target)
    {
        base.Init(target);
        Vector3 randomTorque = Random.onUnitSphere * torqueForce;
        rigidbody.AddTorque(torqueForce, ForceMode2D.Impulse);
    }

    protected override void DestroyBullet()
    {
        ParticlePool.Instance.PlayExplossion(transform.position,radiusExplose);
        base.DestroyBullet();
    }
}