using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    [SerializeField] private List<WeaponHandler> weaponHandlers;

    private void Awake()
    {
        foreach (var weaponHandler in weaponHandlers)
        {
            weaponHandler.Init();
        }
    }

    public void UnlockWeapon(WeaponInfoData weaponInfoData)
    {
        weaponHandlers.Find(x => x.weaponInfoData == weaponInfoData).isLocked = false;
    }

    public List<WeaponHandler> UnlockedWeapons => weaponHandlers.FindAll(x => !x.isLocked);
}