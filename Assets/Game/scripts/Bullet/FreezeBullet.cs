using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class FreezeBullet : BaseBullet
{
    public override Type Type => typeof(FreezeBullet);

    [SerializeField] FreezeRay _laser;
    [Inject] private DiContainer diContainer;

    public override void Init(Transform target)
    {
        base.Init(target);
        if (target != null)
        {
            FreezeRay laser = diContainer.InstantiatePrefabForComponent<FreezeRay>(_laser, transform.position, Quaternion.identity, null);
            laser.transform.GetComponent<FreezeRay>().RayShoot(target, WeaponUpgradeData, transform);
        }
        DestroyBullet();
    }


}