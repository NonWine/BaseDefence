using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class FreezeRay : MonoBehaviour
{
    [SerializeField] protected float _gunRange;
    [SerializeField] protected LineRenderer _laserLine;
    [SerializeField] protected Transform _enemy;
    [SerializeField] protected LayerMask _layerMask;
    [Inject] protected PlayerHandler playerHandler;
    [Inject] protected EnemyFactory enemyFactory;
    protected float _damageTimer;
    protected float _shootingTimer;
    protected Vector3 _target;
    protected Ray _ray;
    protected RaycastHit2D[] hits;
    protected WeaponUpgradeData WeaponUpgradeData;
    public bool isShhoted;

    public void RayShoot(Transform target, WeaponUpgradeData weaponUpgradeData)
    {

        WeaponUpgradeData = weaponUpgradeData;
        transform.SetParent(playerHandler.Player.transform);
        _enemy = target;
        _target = target.position;


    }
    private BaseEnemy GetNearlestEnemy(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(thisTarget.transform.position, e.transform.position))
            .FirstOrDefault(x => x.IsDeath == false);


        return nearestEnemy;
    }
    private void LateUpdate()
    {
        if (_enemy == null || _enemy.GetComponent<BaseEnemy>().IsDeath)
        {
            var nearestEnemy = GetNearlestEnemy(transform);
            if (nearestEnemy == null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                _enemy = nearestEnemy.transform;
            }
        }

        _shootingTimer += Time.deltaTime;
        _damageTimer += Time.deltaTime;

        if (_shootingTimer >= WeaponUpgradeData.GetStat(StatName.LaserDuration).CurrentValue)
        {
            Destroy(gameObject);
            return;
        }

        // �������� ����� �� �����, ���� ����� ������� (���������, ����� ��� ���������� �����)
        Vector3 targetPoint = _enemy.position; // ����� ��������������� _enemy.GetComponent<Collider>().bounds.center

        // ��������� ������� ������
        Vector3 direction = targetPoint - transform.position;
        _laserLine.SetPosition(0, transform.position);
        _laserLine.SetPosition(1, transform.position + direction.normalized * _gunRange); // ������ �� ������� ������

        RaycastShoot(direction);
    }

    protected virtual void RaycastShoot(Vector3 direction)
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

            _damageTimer = 0;
        }
        else
        {
            // ���� ����� �� �������, ��� ����� � - �� ���� ���� �������� � LayerMask
            Debug.LogWarning("Raycast didn't hit anything but enemy exists");
        }
    }
}