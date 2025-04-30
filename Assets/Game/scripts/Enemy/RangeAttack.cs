using DG.Tweening;
using UnityEngine;
using Zenject;

public class RangeAttack : IAttackable
{
    private BulletFactory _bulletFactory;
    private Transform _bulletSpawnPos;
    private BaseEnemy _enemy;
    private Target _target;

    
    public RangeAttack(BaseEnemy enemy, BulletFactory bulletFactory, Transform bulletSpawnPos, Target target)
    {
        _bulletFactory = bulletFactory;
        _bulletSpawnPos = bulletSpawnPos;
        _enemy = enemy;
        _target = target;
    }
    
    public void Attack(IDamageable damageable)
    {
        //var currentTarget = new Vector3(_bulletSpawnPos.position.x, target.transform.position.y, target.transform.position.z);
        var bullet = _bulletFactory.Create(typeof(EnemyBullet));
        bullet.transform.position = _bulletSpawnPos.position;
        bullet.Init(_enemy.CurrentDamage, _target.transform);
        //bullet.Init(_enemy.CurrentDamage, _unitAgrRadius.CurrentAgredTransform);
    }
}
