using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class ShotGunBullet : BaseBullet
{
    public override Type Type => typeof(ShotGunBullet);
    [SerializeField] private PelletFlying _pellet;
    [SerializeField] private float _maxSpreadAngle;
    [Inject] private DiContainer diContainer;
    private float angleOffset;
    public override void Init(Transform target)
    {
        base.Init(target);
        float pelletCount = WeaponUpgradeData.GetStat(StatName.PelletCount).CurrentValue;
        if(target != null)
        {
            Vector2 baseDirection = (target.position - transform.position).normalized;
            for(int i = 0; i < WeaponUpgradeData.GetStat(StatName.PelletCount).CurrentValue; i++)
            {
                angleOffset = Mathf.Lerp(-_maxSpreadAngle, _maxSpreadAngle, (float)i/(pelletCount-1));
                Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);

                Vector2 pelletDirection = rotation * baseDirection;
                PelletFlying pellet = diContainer.InstantiatePrefabForComponent<PelletFlying>(_pellet, transform.position, Quaternion.identity, null);
                pellet.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                pellet.Init(pelletDirection, WeaponUpgradeData);
            }
        }
        DestroyBullet();
    }
    private Vector2 RotateVector2(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}
