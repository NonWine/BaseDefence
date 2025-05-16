using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZaborHealer : StaticWeaponObj
{
    [Inject] private Target target;
    private float _healTimer;

    private void Update()
    {
        _healTimer += Time.deltaTime;
       // if(_healTimer >= WeaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue)
        {
            if(target.IsDamaged)
            {
                return;
            }
            target.Heal(WeaponUpgradeData.GetStat(StatName.healBonus).CurrentValueInt);
            _healTimer = 0;
        }
    }
}