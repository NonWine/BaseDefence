using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelData", fileName = "WaveData", order = 0)]

public class LevelData : ScriptableObject
{
    [SerializeField] public List<WaveDataConfig> wavesData;

}