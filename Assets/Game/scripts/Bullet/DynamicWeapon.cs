 using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DynamicWeapon", fileName = "DynamicWeapon", order = 0)]
public class DynamicWeapon : WeaponInfoData
{
    [TabGroup("General")]
    public BaseBullet baseBullet;
    
    
    protected override void Awake()
    {
        base.Awake();
        WeaponsGeneralType = WeaponsGeneralType.Active;
    }
}