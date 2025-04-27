using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour
{
   [SerializeField] private  List<WaveDataConfig> wavesData;
   [SerializeField] private  EnemySpawner _spawner;
   [SerializeField] private float wavesDuration;
   [SerializeField] private WaveTimer waveTimer;
   [Inject] private  IPlayerMonitor _playerMonitor;
   [Inject] public PlayerHandler _player;
   private int currentWaveIndex;
   private bool _waveActive;
   public int CurrentTime => waveTimer.CurrentTime;
   public WaveDataConfig CurrentWave => wavesData[currentWaveIndex];

   private void Start()
   {
       StartWave();
        waveTimer.StartTimer(CurrentWave.waveDuration);
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
        _player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
        _spawner.StartSpawning(CurrentWave);
        waveTimer.StartTimer(CurrentWave.waveDuration);
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
        _player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Idle);
        _spawner.StopSpawning();
        // Trigger end of wave events here
    }
}