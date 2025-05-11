using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Zenject;
public class LaserBullet : BaseBullet
{
    public override Type Type => typeof(LaserBullet);
    [SerializeField] LaserRay _laser;
    [Inject] private DiContainer diContainer;
    
    public override void Init(Transform target)
    {
        base.Init(target);
        if (target != null)
        {
            LaserRay laser = diContainer.InstantiatePrefabForComponent<LaserRay>(_laser, transform.position, Quaternion.identity, null);
            laser.transform.GetComponent<LaserRay>().RayShoot(target, WeaponUpgradeData);
        }
        DestroyBullet();
    }
}
