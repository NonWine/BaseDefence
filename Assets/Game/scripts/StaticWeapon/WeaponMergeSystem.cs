using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponMergeSystem : MonoBehaviour
{
    [SerializeField] private MergeWeaponContainer[] mergeWeaponContainers;
    [SerializeField] private SlotsView[] SlotsViews;
    public List<MergeWeaponData> MergeWeaponsData { get; set; } = new List<MergeWeaponData>();

    public void CheckGetMergeWeapon(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData is MergeWeaponData data)
        {
            if (MergeWeaponsData.Contains(data))
            {
              var mergeContainer =  mergeWeaponContainers.ToList().Find(x => x.MergeWeaponData.name == data.name);
              foreach (var mergeContainerDynamicWeapon in mergeContainer.Weapons)
              {
                  mergeContainerDynamicWeapon.WeaponUpgradeData.IsUnLocked = false;   
                  foreach (var slotsView in SlotsViews)
                  {
                      slotsView.RemoveSlot(mergeContainerDynamicWeapon);
                  }
              }
              MergeWeaponsData.Remove(data);
            }
            
        }
    }

    public void TryUnlockMergeWeapon()
    {
        foreach (var mergeWeaponContainer in mergeWeaponContainers)
        {
            if(mergeWeaponContainer.isUnLocked) continue;
            
            var isAllCardsMax = mergeWeaponContainer.Weapons.ToList().All(x => x.WeaponUpgradeData.IsCardLevelMax);
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
    [FormerlySerializedAs("DynamicWeapons")] public WeaponInfoData[] Weapons;
    public MergeWeaponData MergeWeaponData;
    public bool isUnLocked;
    
}