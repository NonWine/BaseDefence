using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StinkyBall : BaseBullet
{
    [SerializeField] private float torqueForce = 10f;
    [SerializeField] private GameObject damageableZone;

    public override Type Type => typeof(StinkyBall);

    protected override void OnTriggerEnter2D(Collider2D other)
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
            Debug.Log("creating perdunets");
            Instantiate(damageableZone, transform.position, Quaternion.identity);
            
            DestroyBullet();
        }

    }



    public override void Init(Transform target)
    {
        base.Init(target);
        
        rigidbody.AddTorque(torqueForce, ForceMode2D.Impulse);
    }

    protected override void DestroyBullet()
    {
        base.DestroyBullet();
    }
}
