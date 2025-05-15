using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Mine : StaticWeaponObj
{
    [SerializeField] private float delayActivateTime = 0.4f;
    private bool isActivated;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        Invoke(nameof(Activate), delayActivateTime);
    }

    private void Activate()
    {
        isActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent(out IDamageable damageable) && isActivated)
        {
            damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
            ParticlePool.Instance.PlayExplossion(transform.position,4f);
            Destroy(gameObject);
        }
    }
}