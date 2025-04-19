using UnityEngine;

[CreateAssetMenu()]
public class WeaponInfoData : ScriptableObject
{
    public Sprite image;
    public SkillType SkillType;
    public int MaxLevel = 6;
}

public enum SkillType
{
    Active,
    Passive
}