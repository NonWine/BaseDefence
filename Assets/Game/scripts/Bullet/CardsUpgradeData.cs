using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "CardsUpgradeData", fileName = "CardsUpgradeData", order = 0)]
public class CardsUpgradeData : SerializedScriptableObject
{
    [TabGroup("Upgrades")] [InlineProperty]
    [ListDrawerSettings(DraggableItems = false, NumberOfItemsPerPage = 3)]  public List<CardUpgradeInfo> Upgrades = new List<CardUpgradeInfo>();
    [TabGroup("Weapons")] [InlineProperty]
    [SerializeField] public List<DynamicWeapon> DynamicWeapons = new List<DynamicWeapon>();

    public CardUpgradeInfo GetCardUpgradeRandom()
    {
      return Upgrades[Random.Range(0, Upgrades.Count)];
    }

    public bool IsRightWeapon(WeaponInfoData weaponInfoData)
    {
        foreach (var dynamicWeapon in DynamicWeapons)
        {
            if (weaponInfoData == dynamicWeapon)
            {
                return true;
            }
        }

        return false;
    }
}