using System;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class ShokerLauncher : BaseBullet
{
    public override Type Type => typeof(ShokerLauncher);
    
    [SerializeField] private LayerMask damageableMask;
    private Collider2D[] _overlapResults = new Collider2D[20];
    [SerializeField] private ShockerBullet ShockerBullet;
    [Inject] private DiContainer DiContainer;

    private float radiusExplose => WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue;
    

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, radiusExplose, _overlapResults,damageableMask);
            for (int i = 0; i < count; i++)
            {
                if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
                {
                    DiContainer.InstantiatePrefabForComponent<ShockerBullet>(ShockerBullet, transform.position,
                        Quaternion.identity, null).Init(_overlapResults[i].transform);
                    
                }
            }
            DestroyBullet();
        }
        
    }
    
    

    protected override void DestroyBullet()
    {
       // ParticlePool.Instance.PlayExplossion(transform.position,radiusExplose);
        base.DestroyBullet();
    }
}