using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : PoolableFactory<Type,BaseEnemy>
{
    private GameController _gameController;

    public List<BaseEnemy> Enemies => _objectPool._inActiveUnits;

    public EnemyFactory(Dictionary<Type, BaseEnemy> prefabs, DiContainer container, 
        ObjectPoolTemplate<Type, BaseEnemy> objectPoolTemplate, GameController gameController)
        : base(prefabs, container, objectPoolTemplate)
    {
        _gameController = gameController;
    }


    public override BaseEnemy Create(Type type)
    {
      var unit =  base.Create(type);
        _gameController.RegisterInTick(unit);
        
        return unit;
    }
}

