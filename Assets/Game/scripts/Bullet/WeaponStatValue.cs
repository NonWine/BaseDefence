using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable] [InlineProperty]
public class WeaponStatValue
{
    [JsonProperty] [SerializeField] private float BaseValue;
    [JsonProperty] [SerializeField] private float Modificator;
    [JsonProperty] private int UpgradeLevel = 1;
    [JsonProperty] [field: SerializeField] public StatName StatName { get; private set; }

    [ShowInInspector, ReadOnly]  public float CurrentValue => BaseValue * (1 + Modificator * UpgradeLevel);

    public void ResetLevel() => UpgradeLevel = 1;

    public virtual void UpgradeValue()
    {
        UpgradeLevel++;
    }
}