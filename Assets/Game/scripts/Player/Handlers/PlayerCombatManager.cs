using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerCombatManager : MonoBehaviour
{
    [Inject] private WeaponCardManagerView _weaponCardManagerView;
    [SerializeField] private List<DynamicWeaponHandler> weaponHandlers;

    private void Awake()
    {
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