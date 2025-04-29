using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WaveManager : MonoBehaviour
{
   [SerializeField] private  List<WaveDataConfig> wavesData;
   [SerializeField] private  EnemySpawner _spawner;
   [SerializeField] private WaveSliderView waveSliderView;
   [SerializeField] private Button startWaveButton;
   [Inject] private  IPlayerMonitor _playerMonitor;
   [Inject] public PlayerHandler _player;
   
   private int currentWaveIndex;
   private bool _waveActive;
   public float CurrentTime;
   public WaveDataConfig CurrentWave => wavesData[currentWaveIndex];
   
   
    private void OnEnable()
    {   
        startWaveButton.onClick.AddListener(StartWave);
    }
    private void OnDisable()
    {
        startWaveButton.onClick.RemoveListener(StartWave);
    }
    
    [Button]
    public void StartWave()
    {
        CurrentTime = CurrentWave.waveDuration;
        _waveActive = true;
        startWaveButton.interactable = false;
        _player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
        _spawner.StartSpawning(CurrentWave);
        waveSliderView.SetWaveData(CurrentWave.waveDuration);
    }

    public void Update()
    {
        if (!_waveActive) return;

        waveSliderView.UpdateSlider(CurrentWave.waveDuration - CurrentTime);
        _spawner.UpdateSpawner(Time.deltaTime);
        if (CurrentTime <= 0f)
        {
            EndWave();
        }
        else
        {
            CurrentTime -= Time.deltaTime;
        }

    }

    public void UpdateWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex == wavesData.Count)
            currentWaveIndex = 0;
    }

    private void EndWave()
    {
        UpdateWave();
        startWaveButton.interactable = true;
        _waveActive = false;
        _spawner.StopSpawning();
        // Trigger end of wave events here
    }
}