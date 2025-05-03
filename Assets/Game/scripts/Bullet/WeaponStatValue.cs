using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable] [InlineProperty]
public class WeaponStatValue
{
    [SerializeField] private float BaseValue;
    [SerializeField] private float Modificator;
    [field: SerializeField] public StatName StatName { get; private set; }

    private int UpgradeLevel;
    public float CurrentValue { get; private set; }
    
    

    public virtual void UpgradeValue()
    {
        UpgradeLevel++;
        CurrentValue = BaseValue * (1 + Modificator * UpgradeLevel);
    }
}