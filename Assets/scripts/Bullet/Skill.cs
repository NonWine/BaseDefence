using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    
    protected BaseWeaponStats _baseWeaponStats;
    
    public WeaponAttribute Attribute(ValueAttribute valueAttribute) => _baseWeaponStats.GetAtribute(valueAttribute);
    
    public WeaponInfoData WeaponDataOld => _baseWeaponStats.WeaponInfoData;

    public BaseWeaponStats BaseWeaponStats => _baseWeaponStats;

    private void Awake()
    {
        _baseWeaponStats = GetComponent<BaseWeaponStats>();
    }
}