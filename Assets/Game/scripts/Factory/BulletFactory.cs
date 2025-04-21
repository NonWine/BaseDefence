using System;
using System.Collections.Generic;
using Zenject;

public class BulletFactory : PoolableFactory<Type,BaseBullet>
{
    public BulletFactory(Dictionary<Type, BaseBullet> prefabs, DiContainer container, ObjectPoolTemplate<Type, BaseBullet> objectPoolTemplate) 
        : base(prefabs, container, objectPoolTemplate)
    {
        
    }

    

}