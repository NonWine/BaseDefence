using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;

public class LaserRay : MonoBehaviour
{
    [SerializeField] private Transform _laserOrigin;
    [SerializeField] private float _gunRange;
    [SerializeField] private LineRenderer _laserLine;
    [SerializeField] private Transform _enemy;
    [SerializeField] private LayerMask _layerMask;
    [Inject] private PlayerHandler playerHandler;
    [Inject] private EnemyFactory enemyFactory;
    private float _damageTimer;
    private float _shootingTimer;
    private Vector3 _target;
    private Ray _ray;
    private RaycastHit[] hits;
    private WeaponUpgradeData WeaponUpgradeData;

    public void RayShoot(Transform target, WeaponUpgradeData weaponUpgradeData)
    {

        WeaponUpgradeData = weaponUpgradeData;
        transform.SetParent(playerHandler.Player.transform);
        _laserOrigin = transform;
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
        if(_enemy == null || _enemy.GetComponent<BaseEnemy>().IsDeath)
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
    
        // Отримуємо точку на ворозі, куди треба стріляти (наприклад, центр або спеціальну точку)
        Vector3 targetPoint = _enemy.position; // Можна використовувати _enemy.GetComponent<Collider>().bounds.center
    
        // Оновлюємо позиції лазера
        Vector3 direction = targetPoint - transform.position;
        _laserLine.SetPosition(0, transform.position);
        _laserLine.SetPosition(1, transform.position + direction.normalized * _gunRange); // Просто до позиції ворога
    
        // Raycast для перевірки попадання
        _ray = new Ray(transform.position, direction.normalized);
        Debug.DrawRay(transform.position, direction, Color.green, 0.01f);
        hits = Physics.RaycastAll(_ray,_gunRange , _layerMask);
    
        if (hits.Length > 0)
        {
            if (_damageTimer >= WeaponUpgradeData.GetStat(StatName.DamageInterval).CurrentValue)
            {
                foreach(var hit in hits)
                {
                    if(hit.transform.TryGetComponent<IDamageable>(out var damageable))
                    {
                        damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
                    }
                }
                _damageTimer = 0;
            }
        }
        else
        {
            // Якщо нічого не влучило, але ворог є - це може бути проблема з LayerMask
            Debug.LogWarning("Raycast didn't hit anything but enemy exists");
        }
    }

}
