using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[System.Serializable] [InlineProperty] [HideLabel]
public  class WeaponUpgradeData
{
    [TabGroup("Upgrade Queue")]
    [JsonIgnore] [InlineProperty] [SerializeField] private List<StatName> UpgradeQueue;

    [TabGroup("BaseStats Configuration")] [SerializeField] [JsonProperty]
    public List<WeaponStatValue> BaseStats;
    
    [field: ProgressBar(1,10)] [field: SerializeField]  public int CardLevelMax { get; set; }

    [JsonProperty] [field: SerializeField,ReadOnly]  public int CurrentLevel { get; set; } = 1;
    [JsonProperty] [field: SerializeField,ReadOnly]  public int CardLevel { get; set; } 

    [JsonProperty] [field: SerializeField, ReadOnly] public bool IsLevelMax { get; private set; }
    [JsonProperty] [field: SerializeField, ReadOnly] public bool IsCardLevelMax { get; private set; }
    [JsonProperty] [field: SerializeField, ReadOnly] public bool IsUnLocked { get;  set; }


    [Button]
    public void Upgrade()
    {
        var weaponValue = BaseStats.Find(x => x.StatName == UpgradeQueue[CurrentLevel-1]);
        if (weaponValue != null)
        {
            weaponValue.UpgradeValue();
            CurrentLevel++;
            if (CurrentLevel > UpgradeQueue.Count)
            {
                IsLevelMax = true;
            }
        }
        
    }
    
    [Button]
    public void ForceSave()
    {
            
    }
    
    public void UpgradeCardLevel()
    {
        CardLevel++;
        if (CardLevelMax == CardLevel)
        {
            IsCardLevelMax = true;
        }
    }

    public WeaponStatValue GetStat(StatName statName)
    {
        var statValue = BaseStats.Find(x => x.StatName == statName);
        if (statValue == null)
        {
            Debug.LogError(statName.ToString() + "Doe not exist");
            return null;
        }
        
        return statValue;
    }
    
    public bool IsHaveStat(StatName statName) => BaseStats.Find(x => x.StatName == statName) != null;
    
    

    public void ResetData()
    {
        IsLevelMax = false;
        IsCardLevelMax = false;
        IsUnLocked = false;
        CurrentLevel = 1;
        CardLevel = 0;
        foreach (var weaponStatValue in BaseStats)
        {
            weaponStatValue.ResetLevel();
            weaponStatValue.BonusedValue = 0f;
        }
    }

    public StatName CurrentUpgradeStat() => UpgradeQueue[CurrentLevel-1];

    public void Init(List<WeaponStatValue> weaponStatValues, WeaponUpgradeData weaponUpgradeData)
    {
        CurrentLevel = weaponUpgradeData.CurrentLevel;
        IsLevelMax = weaponUpgradeData.IsLevelMax;
        IsUnLocked = weaponUpgradeData.IsUnLocked;
        CardLevel = weaponUpgradeData.CardLevel;
        
        for (var i = 0; i < weaponStatValues.Count; i++)
        {
            if (BaseStats.Count > i)
            {
                
                BaseStats[i].Init(weaponStatValues[i].Level, weaponStatValues[i].BonusedValue);
            }
        }
    }
    
}