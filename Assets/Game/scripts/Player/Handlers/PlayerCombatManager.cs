using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerCombatManager : MonoBehaviour
{
    [Inject] private WeaponCardManagerView _weaponCardManagerView;
    [Inject] private WeaponInfoData[] weaponInfoDatas;
    [SerializeField, ReadOnly] private List<DynamicWeaponHandler> weaponHandlers;
    [SerializeField] public DynamicWeapon defaultWeapon;
    public float DistanceToAgr = 50f;
    

 
    public void Init()
    {
        weaponHandlers = new List<DynamicWeaponHandler>();
        foreach (var weaponInfoData in weaponInfoDatas)
        {
            if (weaponInfoData is DynamicWeapon dynamicWeapon)
            {
                var handler = new DynamicWeaponHandler(dynamicWeapon);
                    weaponHandlers.Add(handler);
                    if (dynamicWeapon.WeaponUpgradeData.IsUnLocked)
                        handler.isLocked = false;
                    if (dynamicWeapon.WeaponName == defaultWeapon.WeaponName)
                    {
                        handler.isLocked = false;
                        defaultWeapon.WeaponUpgradeData.IsUnLocked = true;

                    }
            }
        }
        _weaponCardManagerView.OnGetWeaponEvent += SetWeaponCard;
    }

 

    private void OnDestroy()
    {
        _weaponCardManagerView.OnGetWeaponEvent -= SetWeaponCard;
    }

    private void SetWeaponCard(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData is DynamicWeapon dynamicWeapon)
        {
            UnlockWeapon(dynamicWeapon);
        }
    }

    public void UnlockWeapon(DynamicWeapon weaponInfoData)
    {
        weaponHandlers.Find(x => x.weaponInfoData == weaponInfoData).isLocked = false;
    }
    

    public List<DynamicWeaponHandler> UnlockedWeapons => weaponHandlers.FindAll(x => !x.isLocked);
}