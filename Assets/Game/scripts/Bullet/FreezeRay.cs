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
    [Inject] protected EnemyFactory enemyFactory;
    [Inject] protected PlayerHandler _playerHandler;
    protected float _damageTimer;
    protected float _shootingTimer;
    protected Vector3 _target;
    protected Ray _ray;
    protected RaycastHit2D[] hits;
    protected WeaponUpgradeData WeaponUpgradeData;
    public bool isShhoted;
    private Vector3 _laserOrigin;
    private Vector3 _currentDirection;
    [SerializeField] private float _rotationSpeed = 5;
    private Vector3 previousPos;
    private float _currentLength;
    private float _currentLengthStart;
    [SerializeField] float _extensionSpeed = 5f;
    [SerializeField] float _currentLengthStartDivider = 3;
    private float _extensionTimer; 
    [SerializeField] private float _extensionDuration = 0.5f;
    protected Vector3 direction;

    public void RayShoot(Transform target, WeaponUpgradeData weaponUpgradeData, Transform startPos)
    {

        WeaponUpgradeData = weaponUpgradeData;
        _enemy = target;
        _target = target.position;
        _laserOrigin = startPos.position;
        //_laserOrigin.SetParent(_playerHandler.Player.bulletStartPoint);
        previousPos = _enemy.position;

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
        /*if (_enemy == null || _enemy.GetComponent<BaseEnemy>().IsDeath)
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
        }*/
        
        _shootingTimer += Time.deltaTime;

        if (_shootingTimer >= WeaponUpgradeData.GetStat(StatName.LaserDuration).CurrentValue)
        {
            Destroy(gameObject);
            return;
        }
        /*if (_enemy == null || _enemy.GetComponent<BaseEnemy>().IsDeath)
        {

        }
        else
        {

            previousPos = _enemy.position;
        }*/



        // �������� ����� �� �����, ���� ����� ������� (���������, ����� ��� ���������� �����)
        Vector3 targetPoint = previousPos; // ����� ��������������� _enemy.GetComponent<Collider>().bounds.center

        // ��������� ������� ������
        
        direction = targetPoint - _laserOrigin;
        _currentLength = Mathf.Lerp(_currentLength, _gunRange,
       _extensionSpeed * Time.deltaTime);
        /*_currentLengthStart = Mathf.Lerp(0, _gunRange,
       _extensionSpeed/_currentLengthStartDivider * Time.deltaTime);*/
        _extensionTimer += Time.deltaTime;
        _currentLengthStart = Mathf.Clamp(_extensionTimer / _extensionDuration, 0f, 1f) * _gunRange;
        /*_currentDirection = Vector3.Lerp(_currentDirection, direction,
            _rotationSpeed * Time.deltaTime);*/
        _laserLine.SetPosition(0, _laserOrigin + direction.normalized * _currentLengthStart);
        //_laserLine.SetPosition(0, _laserOrigin.position);

       _laserLine.SetPosition(1, _laserOrigin + direction.normalized * _currentLength); // ������ �� ������� ������
        if(isShhoted)
        {
            return;
        }
        Invoke("RaycastShoot", 0.3f);
    }

    protected virtual void RaycastShoot()
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
    }
}