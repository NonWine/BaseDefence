using UnityEngine;

public abstract class StaticWeaponObj : MonoBehaviour
{
    private WeaponInfoData weaponInfoData;

    protected WeaponUpgradeData WeaponUpgradeData => weaponInfoData.WeaponUpgradeData;

    public void Init(WeaponInfoData weaponInfoData)
    {
        this.weaponInfoData = weaponInfoData;
    }
    
}