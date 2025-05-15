using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable] [InlineProperty]
public class WeaponStatValue
{
    [JsonIgnore]  [field: SerializeField] public float BaseValue { get; private set; }
   [JsonIgnore]  [SerializeField] private float Modificator;
    [JsonProperty] private int UpgradeLevel = 1;
     [JsonIgnore]  [field: SerializeField] public StatName StatName { get; private set; }

    [JsonIgnore] [ShowInInspector, ReadOnly]  public float CurrentValue => (BaseValue  + BonusedValue) * (1 + Modificator * UpgradeLevel);

     [ShowInInspector, ReadOnly] public float BonusedValue;
   
   public int CurrentValueInt => Mathf.FloorToInt((BaseValue  + BonusedValue) * (1 + Modificator * UpgradeLevel));

   public void Init(int level, float bonuesdValue)
   {
        UpgradeLevel = level;
        BonusedValue = bonuesdValue;

   }

   public void ResetLevel()
   {
        BonusedValue = 0;
        UpgradeLevel = 1;
   } 

    public void ImproveBaseValueByPercent(float value)
    {
        float improvedValue = BaseValue * (value / 100f);
        BonusedValue += improvedValue;
    }
    
    public void ImproveBaseValue(float value)
    {
        BonusedValue += value;
    }

    public int Level => UpgradeLevel;

    public virtual void UpgradeValue()
    {
        UpgradeLevel++;
    }
}