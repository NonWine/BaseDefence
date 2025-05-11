using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : StaticWeaponObj
{
    [SerializeField] private float delayActivateTime = 0.4f;
    private bool isActivated;

    private void Start()
    {
        Invoke(nameof(Activate), delayActivateTime);
    }

    private void Activate()
    {
        isActivated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out IDamageable damageable) && isActivated)
        {
            damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
            ParticlePool.Instance.PlayExplossion(transform.position,4f);
            Destroy(gameObject);
        }
    }
}