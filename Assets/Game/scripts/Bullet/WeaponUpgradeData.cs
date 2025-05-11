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

    [JsonProperty] [field: SerializeField] [ReadOnly] public int CurrentLevel { get; set; } = 1;
    [JsonProperty] [field: SerializeField] [ReadOnly] public int CardLevel { get; set; } = 1;


    [JsonProperty] [field: SerializeField, ReadOnly] public bool LevelMax { get; private set; }
    [JsonProperty] [field: SerializeField, ReadOnly] public bool CardLevelMax { get; private set; }
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
                LevelMax = true;
            }
        }
        
    }
    
    public void UpgradeCardLevel()
    {
        CardLevel++;
        if (GameManager.CardLevelMax == CardLevel)
        {
            CardLevelMax = true;
        }
    }

    public WeaponStatValue GetStat(StatName statName)
    {
        var statValue = BaseStats.Find(x => x.StatName == statName);
        if (statValue == null)
        {
            Debug.LogError(nameof(statName) + "Doe not exist");
            return null;
        }
        
        return statValue;
    }
    
    

    public void ResetData()
    {
        LevelMax = false;
        CurrentLevel = 1;
        foreach (var weaponStatValue in BaseStats)
        {
            weaponStatValue.ResetLevel();
        }
    }

    public StatName CurrentUpgradeStat() => UpgradeQueue[CurrentLevel-1];

    public void Init(List<WeaponStatValue> weaponStatValues, int level)
    {
        CurrentLevel = level;
        
        for (var i = 0; i < weaponStatValues.Count; i++)
        {
            if (BaseStats.Count > i)
            {
                
                BaseStats[i].Init(weaponStatValues[i].Level, weaponStatValues[i].BaseValue);
            }
        }
    }
    
}