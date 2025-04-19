using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PoolableFactory<TType, TEntity> : IFactory<TType, TEntity>
    where TEntity : PoolAble
    where TType : Type
{
    protected Dictionary<TType, TEntity> _prefabs;
    protected DiContainer _container;
    protected ObjectPoolTemplate<TType, TEntity> _objectPool;

    public PoolableFactory(Dictionary<TType, TEntity> prefabs, DiContainer container, ObjectPoolTemplate<TType, TEntity> objectPool)
    {
        _objectPool = objectPool; 
        _prefabs = prefabs;
        _container = container;
    }

    public virtual TEntity Create(TType type)
    {
        if (!_prefabs.ContainsKey(type))
        {
            Debug.LogError($"Prefab for {type} not found!");
            return null;
        }

        if (_objectPool.TryGetFromPool(type, out TEntity entity))
        {
            entity.ResetPool();
            return entity;
        }
        
        
        var instance = _container.InstantiatePrefabForComponent<TEntity>(_prefabs[type]);
        _objectPool._inActiveUnits.Add(instance);

        return instance;
    }

}