using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [SerializeField] private BaseEnemy[] _enemies;
    [SerializeField] private BaseBullet[] _baseBullets;
    [Inject] private GameController _gameController;
    private ObjectPoolTemplate<Type,BaseEnemy> _enemyPool;
    private EnemyFactory _enemyFactory;
    private ObjectPoolTemplate<Type,BaseBullet> _bulletPool;
    private BulletFactory _bulletFactory;
    
    public override void InstallBindings()
    {
        RegisterPool();
        RegisterEnemyFactory();
        RegisterBulletFactory();
    }

    private void RegisterEnemyFactory()
    {
        Dictionary<Type, BaseEnemy> dictionary = new Dictionary<Type, BaseEnemy>();
        foreach (var baseEnemy in _enemies)
        {
            dictionary.Add(baseEnemy.Type, baseEnemy);
        }
        
        _enemyFactory = new EnemyFactory(dictionary, Container, _enemyPool, _gameController);
        Container.BindInstance(_enemyFactory).AsSingle().NonLazy();
    }

    private void RegisterBulletFactory()
    {
        Dictionary<Type, BaseBullet> dictionary = new Dictionary<Type, BaseBullet>();
        foreach (var bullet in _baseBullets)
        {
            dictionary.Add(bullet.Type, bullet);
        }
        
        _bulletFactory = new BulletFactory(dictionary, Container, _bulletPool);
        Container.BindInstance(_bulletFactory).AsSingle().NonLazy();
    }



    private void RegisterPool()
    {
        
        _bulletPool = new ObjectPoolTemplate<Type, BaseBullet>(); 
        Container.BindInstance(_bulletPool).AsSingle();
        
        _enemyPool = new ObjectPoolTemplate<Type, BaseEnemy>(); 
        Container.BindInstance(_enemyPool).AsSingle();
    }
}