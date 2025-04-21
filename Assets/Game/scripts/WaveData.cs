using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public float Duration;
    public float MinSpawnInterval;
    public float MaxSpawnInterval;
    public List<EnemyGroup> EnemyGroups;
}