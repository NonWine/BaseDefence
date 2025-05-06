using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WaveManager : MonoBehaviour
{
   [SerializeField] private  List<WaveDataConfig> wavesData;
   [SerializeField] private  EnemySpawner _spawner;
   [SerializeField] private WaveSliderView waveSliderView;
   [SerializeField] private Button startWaveButton;
   [SerializeField] private TMP_Text waveText;
   [SerializeField] private HealthUI playerHealth;
   [SerializeField] private CanvasGroup menuPopUp;
   [Inject] public PlayerHandler _player;
   [Inject] private GameManager gameManager;

   private int currentWaveIndex;
   private bool _waveActive;
   public float CurrentTime;
   public WaveDataConfig CurrentWave => wavesData[currentWaveIndex];
   
   
    private void OnEnable()
    {
        gameManager.OnLooseEvent += StopWave;
        startWaveButton.onClick.AddListener(StartWave);
    }
    private void OnDisable()
    {
        gameManager.OnLooseEvent -= StopWave;
        startWaveButton.onClick.RemoveListener(StartWave);
    }

    private void Start()
    {
        waveText.text = (currentWaveIndex + 1).ToString() + "/" + wavesData.Count.ToString();
    }

    [Button]
    public void StartWave()
    {
        playerHealth.gameObject.SetActive(true);
        menuPopUp.alpha = 0f;
        menuPopUp.blocksRaycasts = false;
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

    public void StopWave()
    {
        _waveActive = false;
        _spawner.StopSpawning();
        playerHealth.gameObject.SetActive(false);
        menuPopUp.alpha = 1f;
        menuPopUp.blocksRaycasts = true;
    }

    private void UpdateWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex == wavesData.Count)
        {
            gameManager.RestartGame();
            currentWaveIndex = 0;
        }
        
        waveText.text = (currentWaveIndex + 1).ToString() + "/" + wavesData.Count.ToString();
    }

    private void EndWave()
    {
        UpdateWave();
        StopWave();
    }
}