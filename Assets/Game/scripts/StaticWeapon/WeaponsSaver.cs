using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class WeaponsSaver : MonoBehaviour
{
    [SerializeField] private WeaponInfoData[] weaponInfoDatas;
    private WeaponUpgradeDataSaver weaponUpgradeData;
    public bool LoadAtStart;
    [Inject] private WeaponInfoData[] injectedWeapons;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        weaponInfoDatas = injectedWeapons;
        if (!LoadAtStart)
        {
            ResetWeaponSaves();
            return;
        }

        weaponUpgradeData = new WeaponUpgradeDataSaver("weapons");
        var container =   weaponUpgradeData.LoadData();
        
        if(container == null && container.weaponUpgradesData.Count == 0)
            return;
        
        for (var i = 0; i < container.weaponUpgradesData.Count; i++)
        {
            weaponInfoDatas[i].WeaponUpgradeData.Init(container.weaponUpgradesData[i].BaseStats, container.weaponUpgradesData[i]);
        }
    }

    private void OnDestroy()
    {
        SaveWeaponsData();
    }
    
    [Button]
    private void SaveWeaponsData()
    {
        weaponUpgradeData = new WeaponUpgradeDataSaver("weapons");

        WeaponUpgradeDataContainer weaponUpgradeDataContainer = new WeaponUpgradeDataContainer();

        foreach (var weaponInfoData in weaponInfoDatas)
        {
            weaponUpgradeDataContainer.weaponUpgradesData.Add(weaponInfoData.WeaponUpgradeData);
        }

        weaponUpgradeDataContainer.weaponUpgradesData = new List<WeaponUpgradeData>();
        weaponUpgradeDataContainer.weaponUpgradesData.Clear();
        weaponUpgradeData.SaveData(weaponUpgradeDataContainer);
    }
    
    [Button]
    public void ResetWeaponSaves()
    {
        weaponUpgradeData = new WeaponUpgradeDataSaver("weapons");

        WeaponUpgradeDataContainer weaponUpgradeDataContainer = new WeaponUpgradeDataContainer();

        foreach (var weaponInfoData in weaponInfoDatas)
        {
            weaponInfoData.WeaponUpgradeData.ResetData();
        }
        weaponUpgradeData.SaveData(weaponUpgradeDataContainer);
        PlayerPrefs.DeleteAll();
        SaveWeaponsData();
    }
}

