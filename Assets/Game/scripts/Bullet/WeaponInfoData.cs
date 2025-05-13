using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponInfoData : SerializedScriptableObject
{
    [TabGroup("General")] [PreviewField] [LabelText("Icon")] public Sprite image;
    [TabGroup("General")] public string WeaponName;
    [TabGroup("General")] public string description;
    [TabGroup("General")] [ReadOnly] [ShowInInspector] public  WeaponsGeneralType WeaponsGeneralType { get; set; }

    [TabGroup("Weapon Upgrade")] [SerializeField] public WeaponUpgradeData WeaponUpgradeData;
    
    public CardUpgradeInfo CardUpgradeInfo { get; set; }
   
    [Button(SdfIconType.Bluetooth)] [LabelText("Force Update Data")]
    protected  virtual void Awake()
    {
        
    }
    
    
}


public enum WeaponsGeneralType
{
    Active,
    Static,
    Merge
}