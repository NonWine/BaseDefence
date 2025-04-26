using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerCombatManager : MonoBehaviour
{
    [Inject] private WeaponManagerView weaponManagerView;
    [SerializeField] private List<WeaponHandler> weaponHandlers;

    private void Awake()
    {
        weaponManagerView.OnGetWeaponEvent += SetWeapon;
        foreach (var weaponHandler in weaponHandlers)
        {
            weaponHandler.Init();
        }
    }

    private void OnDestroy()
    {
        weaponManagerView.OnGetWeaponEvent -= SetWeapon;
    }

    private void SetWeapon(WeaponInfoData weaponInfoData)
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

    public List<WeaponHandler> UnlockedWeapons => weaponHandlers.FindAll(x => !x.isLocked);
}