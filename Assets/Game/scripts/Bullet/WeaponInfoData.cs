using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WeaponInfoData", fileName = "WeaponInfoData", order = 0)]
public class WeaponInfoData : ScriptableObject
{
    public Sprite image;
    public BaseBullet baseBullet;
    public int maxLevel = 6;
    public int damage;
    public float coolDown;
    
}

public enum SkillType
{
    Active,
    Passive
}