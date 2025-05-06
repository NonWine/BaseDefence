using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable] [InlineProperty]
public class WeaponStatValue
{
    [JsonProperty] [SerializeField] private float BaseValue;
    [JsonProperty] [SerializeField] private float Modificator;
    [JsonProperty] [field: SerializeField] public StatName StatName { get; private set; }

    private int UpgradeLevel;
    [field: SerializeField, ReadOnly] [JsonProperty]  public float CurrentValue { get; private set; }

    public void ResetLevel() => UpgradeLevel = 0;

    public virtual void UpgradeValue()
    {
        UpgradeLevel++;
        CurrentValue = BaseValue * (1 + Modificator * UpgradeLevel);
    }
}