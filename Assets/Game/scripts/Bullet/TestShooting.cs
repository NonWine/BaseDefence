using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
public class TestShooting : MonoBehaviour, IAttackable, ITickable
{
    [Inject] BulletFactory bulletFactory;
    [Inject] EnemyFactory enemyFactory;
    [SerializeField] private int _damage;
    [SerializeField] private float _interval;
    private float _timer;
    [Inject] private GameController gameController;
    private void Awake()
    {
        gameController.RegisterInTick(this);
    }
    public void Attack(IDamageable damageable)
    {
        _timer += Time.deltaTime;
        if (_timer >= _interval)
        {
            var enemy = GetNearlestEnemy(transform);
            if (enemy == null)
            {
                return;
            }
            var bullet = bulletFactory.Create(typeof(StinkyBall));
            bullet.transform.position = transform.position;
            bullet.Init(_damage, enemy.transform);
            _timer = 0;
        }
    }
    private BaseEnemy GetNearlestEnemy(Transform thisTarget)
    {
        var nearestEnemy = enemyFactory.Enemies
            .OrderBy<BaseEnemy, float>(e => Vector3.Distance(thisTarget.transform.position, e.transform.position))
            .FirstOrDefault(x => x.IsDeath == false);


        return nearestEnemy;
    }
    public void Tick()
    {
        Attack(GetNearlestEnemy(transform));
    }
}
