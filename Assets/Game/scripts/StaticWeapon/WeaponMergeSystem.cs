using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponMergeSystem : MonoBehaviour
{
    [SerializeField] private MergeWeaponContainer[] mergeWeaponContainers;
    public List<MergeWeaponData> MergeWeaponsData { get; set; } = new List<MergeWeaponData>();

    public void CheckGetMergeWeapon(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData is MergeWeaponData data)
        {
            if (MergeWeaponsData.Contains(data))
            {
                MergeWeaponsData.Remove(data);
            }
            
        }
    }

    public void TryUnlockMergeWeapon()
    {
        foreach (var mergeWeaponContainer in mergeWeaponContainers)
        {
            if(mergeWeaponContainer.isUnLocked) continue;
            
            var isAllCardsMax = mergeWeaponContainer.DynamicWeapons.ToList().All(x => x.WeaponUpgradeData.IsCardLevelMax);
            if (isAllCardsMax)
            {
                mergeWeaponContainer.isUnLocked = true;
                MergeWeaponsData.Add(mergeWeaponContainer.MergeWeaponData);
            }
        }
    }
}

[System.Serializable]
public class MergeWeaponContainer
{
    public DynamicWeapon[] DynamicWeapons;
    public MergeWeaponData MergeWeaponData;
    public bool isUnLocked;
    
}