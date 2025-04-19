using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseWeaponStats : MonoBehaviour
{
    [SerializeField] protected WeaponInfoData weaponCardDataOld;

    [SerializeField] protected WeaponAttribute[] _attributes = new WeaponAttribute[]
    {
        new WeaponAttribute(){valueAttribute =  ValueAttribute.Damage},
        new WeaponAttribute(){valueAttribute =  ValueAttribute.Speed},
        new WeaponAttribute(){valueAttribute =  ValueAttribute.CoolDown},
    };
    
    [SerializeField] protected WeaponUpgraderConfig[] _upgradeWeaponType = new WeaponUpgraderConfig[]
    {
        new WeaponUpgraderConfig(){UpgradeAttribute = ValueAttribute.Damage},
        new WeaponUpgraderConfig(){UpgradeAttribute = ValueAttribute.Speed},
        new WeaponUpgraderConfig(){UpgradeAttribute = ValueAttribute.CoolDown},

    };

    public event Action UpgradedAttribute; 

    protected int _level;

    private void Awake()
    {
        _level = CurrentLevel;
        for (int i = 0; i < _attributes.Length; i++)
        {
            _attributes[i].Init(gameObject.name);
        }
        
    }

    public WeaponInfoData WeaponInfoData => weaponCardDataOld;
    

    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt("LevelWeapon" + gameObject.name, _level); }

        protected set
        {
            _level = value;
            PlayerPrefs.SetInt("LevelWeapon" + gameObject.name, _level);
        }
    }
    
    public  int LevelMax => _upgradeWeaponType.Length;
    
    public WeaponAttribute GetAtribute(ValueAttribute valueAttribute)
    {
        foreach (var value in _attributes)
        {
            if (value.valueAttribute == valueAttribute)
                return value;
        }
        Debug.LogWarning($"Not Found value Attribute " + gameObject.name  + " the " + valueAttribute);
        return _attributes[0];
    }

    [EasyButtons.Button]
    public void Upgrade()
    {
        if (_level < LevelMax)
        {
             GetAtribute(_upgradeWeaponType[_level].UpgradeAttribute).CurrentLevel++;
            CurrentLevel++; 
             UpgradedAttribute?.Invoke();
            
        }
    } 
        
}