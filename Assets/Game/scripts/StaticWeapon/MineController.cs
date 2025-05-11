using UnityEngine;

public class MineController : StaticWeaponController
{
    [SerializeField] private Mine minePrefab;
    [SerializeField] private RandomPointInBoxCollider randomPointInBoxCollider;

    protected override void UnLockedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= WeaponInfoData.WeaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue)
        {
            timer = 0f;
         diContainer.InstantiatePrefabForComponent<Mine>(minePrefab, randomPointInBoxCollider.GetRandomPointInBox(), Quaternion.identity, null);
        }
    }
}