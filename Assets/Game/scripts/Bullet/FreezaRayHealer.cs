using UnityEngine;
using Zenject;

public class FreezaRayHealer : FreezeRay
{
    [Inject] private Target Target;
    protected override void RaycastShoot(Vector3 direction)
    {
        // Raycast ��� �������� ���������
        _ray = new Ray(transform.position, direction.normalized);
        Debug.DrawRay(transform.position, direction, Color.green, 0.01f);
        hits = Physics2D.RaycastAll(transform.position, direction, _gunRange, _layerMask);

        if (hits.Length > 0 && !isShhoted)
        {
            isShhoted = true;
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
                    hit.transform.GetComponent<BaseEnemy>()
                        .GetFreezed(WeaponUpgradeData.GetStat(StatName.FreezeDuration).CurrentValue);
                }
            }
            Target.Heal(WeaponUpgradeData.GetStat(StatName.healBonus).CurrentValue);
            _damageTimer = 0;
        }
        else
        {
            // ���� ����� �� �������, ��� ����� � - �� ���� ���� �������� � LayerMask
            Debug.LogWarning("Raycast didn't hit anything but enemy exists");
        }
    }
}