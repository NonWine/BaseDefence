using System;
using UnityEngine;

public class SphereBullet : BaseBullet
{
    [SerializeField] private ParticleSystem[] ps;
    public override Type Type => typeof(SphereBullet);
    
    
    public override void Init(Transform target)
    {
        base.Init(target);
        foreach (var system in ps)
        {
            system.Play();
        }
    }

    
    protected override  void DestroyBullet()
    {
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }
        foreach (var system in ps)
        {
            system.Stop();
        }
        gameObject.SetActive(false);
    }
}