using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StaticWeaponData", fileName = "WeaponInfoData", order = 0)]
public class StaticWeaponData : WeaponInfoData
{
    public  WeaponType WeaponType { get; protected set; }
    
    protected override void Awake()
    {
        base.Awake();
        WeaponsGeneralType = WeaponsGeneralType.Static;
    }
}