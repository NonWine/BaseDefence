using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grenade : BaseBullet
{
    [SerializeField] private float radiusExplose = 5f;
    [SerializeField] private float torqueForce = 10f;
    [SerializeField] private LayerMask damageableMask;
    private Collider[] _overlapResults = new Collider[20];

    public override Type Type => typeof(Grenade);

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, radiusExplose, _overlapResults,damageableMask);
            for (int i = 0; i < count; i++)
            {
                if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
                {
                    targetDamageable.GetDamage(_damage);
                }
            }
            DestroyBullet();
        }
        
    }
    
    

    public override void Init(int damage, Transform target)
    {
        base.Init(damage, target);
        Vector3 randomTorque = Random.onUnitSphere * torqueForce;
        rigidbody.AddTorque(randomTorque, ForceMode.Impulse);
    }

    protected override void DestroyBullet()
    {
        ParticlePool.Instance.PlayExplossion(transform.position,radiusExplose);
        base.DestroyBullet();
    }
}