using System;using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = System.Object;

public class ObjectPoolTemplate<TType, TEntity> 
    where TEntity : PoolAble
    where TType : Type 
{
    [Inject] private PoolableFactory<TType, TEntity> _factory;
    public List<TEntity> _inActiveUnits = new List<TEntity>();

    
    // Очищення пулу
    public void ClearPool() => _inActiveUnits.Clear();
    
    
    public bool TryGetFromPool(TType type, out TEntity entity)
    {
        entity = null;
        foreach (var unit in _inActiveUnits)
        {
            if (!unit.gameObject.activeSelf && Object.Equals(unit.Type, type))
            {
                entity = unit;
                return true;
            }
        }
        return false;
    }

    public void ToPool(TEntity entity) 
    {
        entity.gameObject.SetActive(false);
       //          entity.ReturnToPool();
    }
}