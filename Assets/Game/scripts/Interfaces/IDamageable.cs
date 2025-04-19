using UnityEngine;

public interface IDamageable : ITeamable
{
    void GetDamage(int damage);

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

public interface ITeamable
{
    Team Team { get; }
}