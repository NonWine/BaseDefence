using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

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
    private void Update()
    {
        if(_target == null)
        {
            return;
        }
        _shootingTimer += Time.deltaTime;
        _damageTimer += Time.deltaTime;
        if (_shootingTimer >= _laserDuration)
        {
            Destroy(gameObject);
        }
        
        var direction = _target - _laserOrigin.transform.position; 
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
