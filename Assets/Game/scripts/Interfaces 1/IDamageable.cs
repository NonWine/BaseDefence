﻿using UnityEngine;

public interface IDamageable 
{
    void GetDamage(float damage);

    bool IsDeath { get; }
}

public interface IUnitDamagable : IDamageable
{
    
}

public interface IBuildingDamagable : IDamageable
{
    
}



public interface ITickable
{
    void Tick();
}
