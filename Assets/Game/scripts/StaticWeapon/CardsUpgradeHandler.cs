using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CardsUpgradeHandler : MonoBehaviour
{
    [SerializeField] private CardsUpgradeData[] CardsUpgradeData;
    [SerializeField,ReadOnly] private CardUpgradeInfo currentUpgradeData;
    
    public CardUpgradeInfo GetUpgradeData(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData is DynamicWeapon dynamicWeapon)
        {
            foreach (var cardsUpgradeData in CardsUpgradeData)
            {
                if (cardsUpgradeData.IsRightWeapon(weaponInfoData))
                {
                    currentUpgradeData = cardsUpgradeData.GetCardUpgradeRandom();
                    weaponInfoData.CardUpgradeInfo = currentUpgradeData;
                    return currentUpgradeData;
                }
            }
        }
        else if (weaponInfoData is StaticWeaponData staticWeaponData)
        {
            currentUpgradeData = staticWeaponData.Upgrades[staticWeaponData.WeaponUpgradeData.CardLevel];
            weaponInfoData.CardUpgradeInfo = currentUpgradeData;
            return currentUpgradeData;
        }

        Debug.LogError("There" + weaponInfoData + "doe not exist in any Upgrade template");
        return null;
    }

    public void UpgradeCard(WeaponInfoData weaponInfoData)
    {
        foreach (var bonusInfo in weaponInfoData.CardUpgradeInfo.Bonuses)
        {
            float value = bonusInfo.PercentBonus;
            if (bonusInfo.isNegative)
                value *= -1;
            
            weaponInfoData.WeaponUpgradeData.GetStat(bonusInfo.StatName).ImproveBaseValueByPercent(value);
        }
        
        if (weaponInfoData.CardUpgradeInfo is AdvantageUpgrade advantageUpgrade)
        {
            weaponInfoData.WeaponUpgradeData.GetStat(advantageUpgrade.StatName).ImproveBaseValue(advantageUpgrade.Value);
        }
        
        weaponInfoData.WeaponUpgradeData.UpgradeCardLevel();
    }
}