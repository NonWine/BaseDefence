using System;
using System.Collections.Generic;
using Zenject;

public class ResourcePartObjFactory :  PoolableFactory<Type,ResourcePartObj>
{
    public ResourcePartObjFactory(Dictionary<Type, ResourcePartObj> prefabs, DiContainer container, ObjectPoolTemplate<Type, ResourcePartObj> objectPool) : base(prefabs, container, objectPool)
    {
    }
    
}