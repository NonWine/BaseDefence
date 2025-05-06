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
    [SerializeField] private float _laserDuration;
    [SerializeField] private LineRenderer _laserLine;
    [SerializeField] private Transform _enemy;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damageInterval;
    [Inject] private PlayerHandler playerHandler;
    [Inject] private EnemyFactory enemyFactory;
    private float _damageTimer;
    private float _shootingTimer;
    private Vector3 _target;
    private int _damage;
    private Ray _ray;
    private RaycastHit[] hits;

    public void RayShoot(int damage, Transform target)
    {
        
        transform.SetParent(playerHandler.Player.transform);
        _laserOrigin = transform;
        _enemy = target;
        _target = target.position;
        
        _damage = damage;

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
    
        if (_shootingTimer >= _laserDuration)
        {
            Destroy(gameObject);
            return;
        }
    
        // Отримуємо точку на ворозі, куди треба стріляти (наприклад, центр або спеціальну точку)
        Vector3 targetPoint = _enemy.position; // Можна використовувати _enemy.GetComponent<Collider>().bounds.center
    
        // Оновлюємо позиції лазера
        Vector3 direction = targetPoint - _laserOrigin.position;
        _laserLine.SetPosition(0, _laserOrigin.position);
        _laserLine.SetPosition(1, targetPoint); // Просто до позиції ворога
    
        // Raycast для перевірки попадання
        _ray = new Ray(_laserOrigin.position, direction.normalized);
        hits = Physics.RaycastAll(_ray,_gunRange , _layerMask);
    
        if (hits.Length > 0)
        {
            if (_damageTimer >= _damageInterval)
            {
                foreach(var hit in hits)
                {
                    if(hit.transform.TryGetComponent<IDamageable>(out var damageable))
                    {
                        damageable.GetDamage(_damage);
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
