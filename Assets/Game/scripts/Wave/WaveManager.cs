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
   [SerializeField] private WaveTimer waveTimer;
   [Inject] private  IPlayerMonitor _playerMonitor;
   [Inject] public Player _player;
   private int currentWaveIndex;
   private bool _waveActive;
   public int CurrentTime => waveTimer.CurrentTime;
   public WaveData currentWave => wavesData[currentWaveIndex];

   private void Awake()
   {
       StartWave();
        waveTimer.StartTimer(currentWave.waveDuration);
   }
    private void OnEnable()
    {
        waveTimer.OnEndTime += UpdateWave;
    }
    private void OnDisable()
    {
        waveTimer.OnEndTime -= UpdateWave;
    }
    [Button]
    public void StartWave()
    {
        _waveActive = true;
        _player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
        _spawner.StartSpawning(currentWave);
        waveTimer.StartTimer(currentWave.waveDuration);
    }

    public void Update()
    {
        if (!_waveActive) return;

        _spawner.UpdateSpawner(Time.deltaTime);


    //    if (_playerMonitor.AliveEnemies < 3)
      //      _spawner.SpawnExtraGroup();

    }

    public void UpdateWave()
    {
        EndWave();
        currentWaveIndex++;
        if (currentWaveIndex == wavesData.Count)
            currentWaveIndex = 0;
        StartWave();
    }

    private void EndWave()
    {
        _waveActive = false;
        _player.PlayerStateMachine.ChangeState(PlayerStateKey.Idle);
        _spawner.StopSpawning();
        // Trigger end of wave events here
    }
}