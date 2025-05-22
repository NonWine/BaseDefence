using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StinkyBall : BaseBullet
{
    [SerializeField] private float torqueForce = 10f;
    [SerializeField] protected GameObject damageableZone;

    public override Type Type => typeof(StinkyBall);

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            var cloud= Instantiate(damageableZone, transform.position, Quaternion.identity);
            cloud.GetComponent<StinkyCloud>().Init(WeaponInfoData);
            
            DestroyBullet();
        }

    }



    public override void Init(Transform target)
    {
        base.Init(target);
        
        rigidbody.AddTorque(torqueForce, ForceMode2D.Impulse);
    }

    
}
