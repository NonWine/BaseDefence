using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StaticWeaponData", fileName = "WeaponInfoData", order = 0)]
public class StaticWeaponData : WeaponInfoData
{
    public  WeaponType WeaponType { get; protected set; }

    
    
}