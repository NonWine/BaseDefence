 using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DynamicWeapon", fileName = "DynamicWeapon", order = 0)]
public class DynamicWeapon : WeaponInfoData
{
    [TabGroup("BulletInfo")]
    public BaseBullet baseBullet;
    [TabGroup("BulletInfo")]
    public int damage;
    [TabGroup("BulletInfo")]
    public float coolDown;
    
    
    protected override void Awake()
    {
        base.Awake();
        WeaponsGeneralType = WeaponsGeneralType.Active;
    }
}