using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : PoolableFactory<Type,BaseEnemy>
{
    private GameController _gameController;

    public EnemyFactory(Dictionary<Type, BaseEnemy> prefabs, DiContainer container, ObjectPoolTemplate<Type, BaseEnemy> objectPoolTemplate, GameController gameController)
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


public class BulletFactory : PoolableFactory<Type,BaseBullet>
{
    public BulletFactory(Dictionary<Type, BaseBullet> prefabs, DiContainer container, ObjectPoolTemplate<Type, BaseBullet> objectPoolTemplate) 
        : base(prefabs, container, objectPoolTemplate)
    {
        
    }

    

}
