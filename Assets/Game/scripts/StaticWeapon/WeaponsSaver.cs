using System;
using UnityEngine;
using Zenject;

public class WeaponsSaver : MonoBehaviour
{
    [Inject] private WeaponInfoData[] weaponInfoDatas;
    private WeaponUpgradeDataSaver weaponUpgradeData;
    

    private void Start()
    {
        weaponUpgradeData = new WeaponUpgradeDataSaver("weapons");
        var container =   weaponUpgradeData.LoadData();
        
        if(container.weaponUpgradesData.Count == 0)
            return;
        
        for (var i = 0; i < container.weaponUpgradesData.Count; i++)
        {
            weaponInfoDatas[i].WeaponUpgradeData.Init(container.weaponUpgradesData[i].BaseStats, container.weaponUpgradesData[i].CurrentLevel);
        }
    }

    private void OnDestroy()
    {
        WeaponUpgradeDataContainer weaponUpgradeDataContainer = new WeaponUpgradeDataContainer();

        foreach (var weaponInfoData in weaponInfoDatas)
        {
            weaponUpgradeDataContainer.weaponUpgradesData.Add(weaponInfoData.WeaponUpgradeData);
        }
        weaponUpgradeData.SaveData(weaponUpgradeDataContainer);
        
    }
}

