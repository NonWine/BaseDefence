using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField] private List<WaveDataConfig> waveDataConfigs = new List<WaveDataConfig>();
    public int CurrentWave { get; private set; }

    public WaveDataConfig GetWave => waveDataConfigs[CurrentWave];

    public event Action OnWaveMaxEvent;

    public void UpdateWave()
    {
        CurrentWave++;
        if (CurrentWave == waveDataConfigs.Count)
        {
            OnWaveMaxEvent?.Invoke();
        }
    }
}