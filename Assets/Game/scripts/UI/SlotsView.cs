using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public  class SlotsView : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    [Inject] private WeaponCardManagerView weaponCardManagerView;
    [Inject] private WeaponInfoData[] weaponsInfoDatas;
    [SerializeField] private WeaponsGeneralType generalType;
    [SerializeField,ReadOnly] private List<WeaponInfoData> weaponsData;

    [SerializeField,ReadOnly] private bool IsMaxSlots = false;
    
    private int currentSlotIndex = 0;
    
    private void Start()
    {
        weaponCardManagerView.OnGetWeaponEvent += AddSlotView;

        foreach (var weaponsInfoData in weaponsInfoDatas)
        {
            if (weaponsInfoData.WeaponUpgradeData.IsUnLocked)
            {
                AddSlotView(weaponsInfoData);
            }
        }
    }

    private void OnDestroy()
    {
        weaponCardManagerView.OnGetWeaponEvent -= AddSlotView;

    }

    protected virtual void AddSlotView(WeaponInfoData weaponInfoData)
    {
        if(weaponInfoData.WeaponsGeneralType != generalType) return;

        if(weaponsData.Find(x => x.image.name == weaponInfoData.image.name))
            return;

   
        
        if (currentSlotIndex < slots.Length)
        {
            slots[currentSlotIndex].enabled = true;
            slots[currentSlotIndex].sprite = weaponInfoData.image;
            weaponsData.Add(weaponInfoData);
            currentSlotIndex++;
        }

        if (currentSlotIndex >= slots.Length)
        {
            IsMaxSlots = true;

            if (generalType == WeaponsGeneralType.Active)
                weaponCardManagerView.FilterDynamicWeapon();
                
            if (generalType == WeaponsGeneralType.Static)
                weaponCardManagerView.FilterStaticWeapon();
        }
       
            
    }


    public void RemoveSlot(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData.WeaponsGeneralType != generalType) return;

        var data = weaponsData.Find(x => x.image.name == weaponInfoData.image.name);
        if (data == null)
        {
            Debug.LogError(weaponInfoData.WeaponName + "can not find in weapons slots");
            return;
        }

        weaponsData.Remove(data);
        IsMaxSlots = false;
        currentSlotIndex = 0;

        // Очищення всіх слотів
        foreach (var slot in slots)
        {
            slot.enabled = false;
            slot.sprite = null;
        }

        // Перерозставлення спрайтів з оновленого списку
        foreach (var weapon in weaponsData)
        {
            if (currentSlotIndex < slots.Length)
            {
                slots[currentSlotIndex].enabled = true;
                slots[currentSlotIndex].sprite = weapon.image;
                currentSlotIndex++;
            }
        }
        
        weaponCardManagerView.UnFilterWeapons(generalType);
    }

}