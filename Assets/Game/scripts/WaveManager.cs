using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour
{
   [SerializeField] private  List<WaveData> wavesData;
   [SerializeField] private  EnemySpawner _spawner;
   [SerializeField] private float wavesDuration;
   [Inject] private  IPlayerMonitor _playerMonitor;
   [Inject] public Player _player;
   private int currentWaveIndex;
   private float _waveTimer;
   private bool _waveActive;
   
   public WaveData currentWave => wavesData[currentWaveIndex];

   private void Awake()
   {
       StartWave();
   }

   [Button]
    public void StartWave()
    {
        _waveTimer = 0f;
        _waveActive = true;
        _player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
        _spawner.StartSpawning(currentWave);
    }

    public void Update()
    {
        if (!_waveActive) return;

        _waveTimer += Time.deltaTime;
        _spawner.UpdateSpawner(Time.deltaTime);

        if (_playerMonitor.AverageKillTime < 1.5f)
            _spawner.TemporarilyIncreaseSpawnRate();

    //    if (_playerMonitor.AliveEnemies < 3)
      //      _spawner.SpawnExtraGroup();

        if (_waveTimer >= wavesDuration)
            EndWave();
    }

    public void UpdateWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex == wavesData.Count)
            currentWaveIndex = 0;
    }

    private void EndWave()
    {
        _waveActive = false;
        _player.PlayerStateMachine.ChangeState(PlayerStateKey.Idle);
        _spawner.StopSpawning();
        // Trigger end of wave events here
    }
}