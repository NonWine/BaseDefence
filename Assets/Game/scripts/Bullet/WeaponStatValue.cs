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

   [JsonIgnore] [ShowInInspector, ReadOnly]  public float CurrentValue => BaseValue * (1 + Modificator * UpgradeLevel);
   
   
   
   public int CurrentValueInt => Mathf.FloorToInt(BaseValue * (1 + Modificator * UpgradeLevel));

   public void Init(int level, float baseValue)
   {
        UpgradeLevel = level;
        //BaseValue = baseValue;
        
   } 

    public void ResetLevel() => UpgradeLevel = 1;

    public void ImproveBaseValueByPercent(float value)
    {
        float improvedValue = BaseValue * (value / 100f);
        BaseValue += improvedValue;
    }
    
    public void ImproveBaseValue(float value)
    {
        BaseValue += value;
    }

    public int Level => UpgradeLevel;

    public virtual void UpgradeValue()
    {
        UpgradeLevel++;
    }
}