using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponInfoData : ScriptableObject
{
    [TabGroup("General")] [PreviewField] [LabelText("Icon")]
    public Sprite image;
    [TabGroup("General")]
    public string WeaponName;
    [TabGroup("General")]
    public string description;

}