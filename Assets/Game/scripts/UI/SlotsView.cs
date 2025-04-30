using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public  class SlotsView : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    [Inject] private WeaponCardManagerView weaponCardManagerView;
    [SerializeField] private WeaponsGeneralType generalType;
    [SerializeField,ReadOnly] private List<WeaponInfoData> weaponsData;

    private int currentSlotIndex = 0;
    
    private void Awake()
    {
        weaponCardManagerView.OnGetWeaponEvent += AddSlotView;
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
    }
}