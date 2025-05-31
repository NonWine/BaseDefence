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
    [Inject] private PlayerHandler _playerHandler;
    [Inject] private EnemyFactory enemyFactory;
    [Inject] private GameManager gameManager;
    [SerializeField] private float _rotationSpeed;
    private float _damageTimer;
    private float _shootingTimer;
    private Vector3 _target;
    private Ray _ray;
    private RaycastHit2D[] hits;
    private WeaponUpgradeData WeaponUpgradeData;
    private Vector3 _currentDirection;
    private float _currentLength; 
    [SerializeField] private float _extensionSpeed = 0.3f; 

    public void RayShoot(Transform target, WeaponUpgradeData weaponUpgradeData, Transform startPos)
    {

        WeaponUpgradeData = weaponUpgradeData;
        _laserOrigin = startPos;
        _enemy = target;
        _target = target.position;
        _currentDirection = (_target - transform.position).normalized;
        _laserOrigin.SetParent(_playerHandler.Player.bulletStartPoint);

    }
    private void OnEnable()
    {
        gameManager.OnLooseEvent += Destroying;
    }
    private void OnDisable()
    {
        gameManager.OnLooseEvent -= Destroying;
    }
    private void Destroying()
    {
        Destroy(gameObject);
    }
    private bool IsEnemyAround(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(thisTarget.transform.position, e.transform.position))
            .FirstOrDefault(x => x.IsDeath == false);
        if(nearestEnemy == null)
        {
            return false;
        }
        if(Vector3.Distance(thisTarget.position, nearestEnemy.transform.position) >=
            _playerHandler.Player.PlayerCombatManager.DistanceToAgr)
        {
            return false;
        }
        return true;
    }
    private void LateUpdate()
    {
        

        var nearestEnemy = _playerHandler.Player.PlayerGiveDamageHandler.CurrentAgredTarget;
        if (nearestEnemy == null || nearestEnemy.GetComponent<BaseEnemy>().IsDeath)
        {
            if(enemyFactory.Enemies.Count() > 0 && IsEnemyAround(_playerHandler.Player.transform))
            {
                return;
            }
            Destroy(gameObject);
            return;
        }
        else
        {
            _enemy = nearestEnemy.transform;
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
        _currentLength = Mathf.Lerp(_currentLength, _gunRange,
        _extensionSpeed * Time.deltaTime);
        _laserLine.SetPosition(0, _laserOrigin.position);
        _laserLine.SetPosition(1, _laserOrigin.position + _currentDirection.normalized * _currentLength); 
    
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
    }

}
