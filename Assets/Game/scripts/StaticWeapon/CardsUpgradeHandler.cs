using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CardsUpgradeHandler : MonoBehaviour
{
    [SerializeField] private CardsUpgradeData[] CardsUpgradeData;
    [SerializeField,ReadOnly] private CardUpgradeInfo currentUpgradeData;
    
    public CardUpgradeInfo GetUpgradeData(WeaponInfoData weaponInfoData)
    {
        foreach (var cardsUpgradeData in CardsUpgradeData)
        {
            if (cardsUpgradeData.IsRightWeapon(weaponInfoData))
            {
                currentUpgradeData = cardsUpgradeData.GetCardUpgradeRandom();
                return currentUpgradeData;
            }
        }

        Debug.LogError("There" + weaponInfoData + "doe not exist in any Upgrade template");
        return null;
    }

    public void UpgradeCard(WeaponInfoData weaponInfoData)
    {
        foreach (var bonusInfo in currentUpgradeData.Bonuses)
        {
            float value = bonusInfo.PercentBonus;
            if (bonusInfo.isNegative)
                value *= -1;
            
            weaponInfoData.WeaponUpgradeData.GetStat(bonusInfo.StatName).ImproveBaseValueByPercent(value);
        }
        
        if (currentUpgradeData is AdvantageUpgrade advantageUpgrade)
        {
            weaponInfoData.WeaponUpgradeData.GetStat(advantageUpgrade.StatName).ImproveBaseValue(advantageUpgrade.Value);
        }
        
        weaponInfoData.WeaponUpgradeData.UpgradeCardLevel();
    }
}