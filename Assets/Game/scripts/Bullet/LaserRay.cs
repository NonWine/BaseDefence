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
    [SerializeField] private float _rotationSpeed;
    private float _damageTimer;
    private float _shootingTimer;
    private Vector3 _target;
    private Ray _ray;
    private RaycastHit2D[] hits;
    private WeaponUpgradeData WeaponUpgradeData;
    private Vector3 _currentDirection;

    public void RayShoot(Transform target, WeaponUpgradeData weaponUpgradeData, Transform startPos)
    {

        WeaponUpgradeData = weaponUpgradeData;
        transform.SetParent(playerHandler.Player.transform);
        _laserOrigin = startPos;
        _enemy = target;
        _target = target.position;
        _currentDirection = (_target - transform.position).normalized;

    }
    private BaseEnemy GetNearlestEnemy(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(thisTarget.transform.position, e.transform.position))
            .FirstOrDefault(x => x.IsDeath == false);
        if(nearestEnemy == null)
        {
            return null;
        }
        if(Vector3.Distance(transform.position, nearestEnemy.transform.position) >= 25)
        {
            return null;
        }
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
    
        
        Vector3 targetPoint = _enemy.position; 
    
        
        Vector3 direction = targetPoint - _laserOrigin.position;
        _currentDirection = Vector3.Lerp(_currentDirection, direction,
            _rotationSpeed * Time.deltaTime);
        _laserLine.SetPosition(0, _laserOrigin.position);
        _laserLine.SetPosition(1, _laserOrigin.position + _currentDirection.normalized * _gunRange); 
    
        _ray = new Ray(_laserOrigin.position, _currentDirection.normalized);
        Debug.DrawRay(_laserOrigin.position, _currentDirection, Color.green, 0.01f);
        hits = Physics2D.RaycastAll(_laserOrigin.position, _currentDirection, _gunRange , _layerMask);
    
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
