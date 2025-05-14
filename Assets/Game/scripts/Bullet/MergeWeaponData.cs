using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MergeWeaponData", fileName = "MergeWeaponData", order = 0)]
public class MergeWeaponData : DynamicWeapon
{
  

    [Button(SdfIconType.Bluetooth)] [LabelText("Force Update Data")]
    protected  override void Awake()
    {
        WeaponsGeneralType = WeaponsGeneralType.Merge;
    }
}