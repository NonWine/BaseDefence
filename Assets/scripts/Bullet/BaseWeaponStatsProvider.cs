using UnityEngine;
using Zenject;

public class BaseWeaponStatsProvider : MonoBehaviour
{
    [Inject] private BaseWeaponStats[] _baseWeaponStats;

    public void SetDebuff(float value, ValueAttribute valueAttribute)
    {
        foreach (var VARIABLE in _baseWeaponStats)
        {
            var weaponAttribute = VARIABLE.GetAtribute(valueAttribute);
            if (weaponAttribute.valueAttribute == valueAttribute)
            {
                float percent = weaponAttribute.Value * (value / 100f);
                weaponAttribute.Debuf = percent;
            }
        }
    }
}