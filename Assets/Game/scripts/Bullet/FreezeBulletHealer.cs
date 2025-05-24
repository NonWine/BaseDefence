using System;
using UnityEngine;
using Zenject;

public class FreezeBulletHealer : BaseBullet
{
    public override Type Type => typeof(FreezeBulletHealer);

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