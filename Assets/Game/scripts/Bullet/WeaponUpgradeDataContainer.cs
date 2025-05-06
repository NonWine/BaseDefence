using System.Collections.Generic;

[System.Serializable]
public class WeaponUpgradeDataContainer
{
    public List<WeaponUpgradeData> weaponUpgradesData;

    public WeaponUpgradeDataContainer()
    {
        weaponUpgradesData = new List<WeaponUpgradeData>();
    }
}