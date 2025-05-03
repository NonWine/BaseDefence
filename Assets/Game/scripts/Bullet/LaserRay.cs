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
        _laserOrigin = playerHandler.Player.bulletStartPoint;
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
    private void Update()
    {
        if(_enemy.GetComponent<BaseEnemy>().IsDeath)
        {
            if(GetNearlestEnemy(transform) == null)
            {
                Debug.Log("nearest enemy is null");
                Destroy(gameObject);
            }
            else
            {
                _enemy = GetNearlestEnemy(transform).transform;
            }

            return;
        }
        _shootingTimer += Time.deltaTime;
        _damageTimer += Time.deltaTime;
        if (_shootingTimer >= _laserDuration)
        {
            Destroy(gameObject);
        }
        
        var direction = _enemy.transform.position - _laserOrigin.transform.position; 
        _ray = new Ray(transform.position, direction.normalized);
        _laserLine.SetPosition(0, _laserOrigin.transform.position);
        
        hits = Physics.RaycastAll(_ray, _gunRange, _layerMask);
        if (hits.Length>0)
        {
            _laserLine.SetPosition(1, direction.normalized * _gunRange);
            if (_damageTimer >= _damageInterval)
            {
                foreach(var hit in hits)
                {
                    hit.transform.GetComponent<IDamageable>().GetDamage(_damage);
                }
                _damageTimer = 0;
            }
            
        }
        else
        {
            _laserLine.SetPosition(1, direction.normalized * _gunRange);
        }

    }
}
