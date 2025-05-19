using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBomb : StaticWeaponObj
{
    [SerializeField] GameObject bombObj;
    [SerializeField] float time;
    private float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {

            damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
        }

    }
}
