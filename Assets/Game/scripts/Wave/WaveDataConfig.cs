using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WaveData", fileName = "WaveData", order = 0)]
public class WaveDataConfig : ScriptableObject
{
    [ProgressBar(1, 5)] public float SpawnInterval;

    public int waveDuration;
    public AnimationCurve intervalCurve;
    public List<EnemySpawnInfo> Enemies;

    

}