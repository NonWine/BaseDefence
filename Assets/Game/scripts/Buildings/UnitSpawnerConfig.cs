using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "UnitSpawnerConfig", menuName = "Configs/UnitSpawnerConfig")]
public class UnitSpawnerConfig : BuildingData
{
    public  BaseEnemy enemyType;
    public float SpawnInterval;
   [Range(0,10)] public int UnitsPerSpawn;
}