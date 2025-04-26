using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WaveData", fileName = "WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    [ProgressBar(1, 5)] public float MinSpawnInterval;
    [ProgressBar(5, 10)] public float MaxSpawnInterval;
    public int waveDuration;

    public List<EnemySpawnInfo> Enemies;

    

}