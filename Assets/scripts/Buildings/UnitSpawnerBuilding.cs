using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public abstract class UnitSpawnerBuilding : Building<UnitSpawnerConfig>
{
    [Inject] private EnemyFactory _enemyFactory;
    [SerializeField] protected SphereCollider PointSpawn;

    public event Action<BaseEnemy> OnSpawnUnit; 
    protected override void Start()
    {
        base.Start();
        if (_buildingData != null)
        {
           StartCoroutine(SpawnUnitsCoroutine());
        }
        else
        {
            Debug.LogWarning("Spawner Config is not assigned!");
        }
    }
    
    protected virtual IEnumerator SpawnUnitsCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < _buildingData.UnitsPerSpawn; i++)
            {
                SpawnUnit();
                yield return new WaitForSeconds(0.1f);
            }
        
            yield return new WaitForSeconds(_buildingData.SpawnInterval);
        }

    }

    protected virtual void SpawnUnit()
    {
        var unit = _enemyFactory.Create(_buildingData.enemyType.GetType());
        unit.transform.position = transform.position;
        unit.transform.rotation =  Quaternion.LookRotation(-transform.forward, Vector3.up);
        unit.SetTeam(Team);
        OnSpawnUnit?.Invoke(unit);
    }
    

}