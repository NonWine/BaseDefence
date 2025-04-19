using DG.Tweening;
using UnityEngine;

public class RangeAttack : IAttackable
{
    private BulletFactory _bulletFactory;
    private Transform _bulletSpawnPos;
    private BaseEnemy _enemy;
    private UnitAgrRadius _unitAgrRadius;
    
    public RangeAttack(BaseEnemy enemy, BulletFactory bulletFactory, UnitAgrRadius unitAgrRadius, Transform bulletSpawnPos)
    {
        _bulletFactory = bulletFactory;
        _bulletSpawnPos = bulletSpawnPos;
        _enemy = enemy;
        _unitAgrRadius = unitAgrRadius;
    }
    
    public void Attack(IDamageable damageable)
    {
        var bullet = _bulletFactory.Create(typeof(SphereBullet));
        bullet.transform.position = _bulletSpawnPos.position;
        bullet.Init(_enemy.CurrentDamage, _unitAgrRadius.CurrentAgredTransform);
    }
}
