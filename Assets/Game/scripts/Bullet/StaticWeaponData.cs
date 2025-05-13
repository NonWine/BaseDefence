using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StaticWeaponData", fileName = "WeaponInfoData", order = 0)]
public class StaticWeaponData : WeaponInfoData
{
    //[field: SerializeField] public  WeaponType WeaponType { get; protected set; }
    [TabGroup("Level Upgrades")]
    [SerializeField] public List<CardUpgradeInfo> Upgrades = new List<CardUpgradeInfo>();
    
    protected override void Awake()
    {
        base.Awake();
        WeaponsGeneralType = WeaponsGeneralType.Static;

    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        WeaponUpgradeData.CardLevelMax = Upgrades.Count;
    }
#endif
}