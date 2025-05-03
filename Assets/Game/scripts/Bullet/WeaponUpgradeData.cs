using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[System.Serializable] [InlineProperty] [HideLabel]
public  class WeaponUpgradeData
{
    [TabGroup("Upgrade Queue")]
    [InlineProperty] [SerializeField] private List<StatName> UpgradeQueue;

    [TabGroup("BaseStats Configuration")] [SerializeField] 
    [ShowInInspector]  public List<WeaponStatValue> BaseStats;
    
    [field: SerializeField]  [ReadOnly] public int CurrentLevel { get; set; }

    public void Upgrade()
    {
        var weaponValue = BaseStats.Find(x => x.StatName == UpgradeQueue[CurrentLevel]);
        if (weaponValue != null)
        {
            weaponValue.UpgradeValue();
        }
        
    }

    public void Init(List<WeaponStatValue> weaponStatValues, int level)
    {
        CurrentLevel = level;
        BaseStats = weaponStatValues;
    }
    
}