using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StinkyBall : BaseBullet
{
    [SerializeField] private float radiusExplose = 5f;
    [SerializeField] private float torqueForce = 10f;
    [SerializeField] private LayerMask damageableMask;
    [SerializeField] private GameObject damageableZone;
    private Collider[] _overlapResults = new Collider[20];

    public override Type Type => typeof(StinkyBall);

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            /*int count = Physics.OverlapSphereNonAlloc(transform.position, radiusExplose, _overlapResults, damageableMask);
            for (int i = 0; i < count; i++)
            {
                if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
                {
                    targetDamageable.GetDamage(_damage);
                }
            }*/
            //damageableZone.SetActive(true);
            Debug.Log("instantiating stinky cloud");
            Instantiate(damageableZone, transform.position, Quaternion.identity);
            
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
        ParticlePool.Instance.PlayExplossion(transform.position, radiusExplose);
        base.DestroyBullet();
    }
}
