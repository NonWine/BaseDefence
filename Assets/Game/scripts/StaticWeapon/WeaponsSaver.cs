using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(-2)]
public class WeaponsSaver : MonoBehaviour
{
    [SerializeField] private WeaponInfoData[] weaponInfoDatas;
    private WeaponUpgradeDataSaver weaponUpgradeData;
    

    [Inject]
    public void Init()
    {
        weaponUpgradeData = new WeaponUpgradeDataSaver("weapons");
        var container =   weaponUpgradeData.LoadData();
        
        if(container == null)
            return;
        
        for (var i = 0; i < container.weaponUpgradesData.Count; i++)
        {
            weaponInfoDatas[i].WeaponUpgradeData.Init(container.weaponUpgradesData[i].BaseStats, container.weaponUpgradesData[i].CurrentLevel);
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
            weaponUpgradeDataContainer.weaponUpgradesData.Add(weaponInfoData.WeaponUpgradeData);
        }
        weaponUpgradeData.SaveData(weaponUpgradeDataContainer);

    }
}

