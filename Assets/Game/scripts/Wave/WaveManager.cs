using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class WaveManager : MonoBehaviour
{ 
   [SerializeField] private  List<LevelData> levelsData;
   [SerializeField] private  EnemySpawner _spawner;
   [SerializeField] private WaveSliderView waveSliderView;
   [SerializeField] private Button startWaveButton;
   [SerializeField] private TMP_Text waveText;
   [SerializeField] private HealthUI playerHealth;
   [SerializeField] private CanvasGroup menuPopUp;
   [SerializeField] private ParticleSystem waveWinPs;
   [Inject] private EnemyFactory EnemyFactory;
   [Inject] public PlayerHandler _player;
   [Inject] private GameManager gameManager;
   [ReadOnly] public WaveDataConfig CurentWave;
   private int currentLevelIndex;
   private int currentWaveIndex;
   public bool _waveActive, endWave;
   public float CurrentTime;

   public int CurrentLevel => currentLevelIndex;
   
   public WaveDataConfig CurrentWave => levelsData[currentLevelIndex].wavesData[currentWaveIndex];


   public static WaveManager Instance { get; private set; }

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
        Instance = this;
        waveText.text = (currentWaveIndex ).ToString() + "/" + levelsData[currentLevelIndex].wavesData.Count.ToString();
    }

    [Button]
    public void StartWave()
    {
        playerHealth.gameObject.SetActive(true);
        menuPopUp.alpha = 0f;
        menuPopUp.blocksRaycasts = false;
        CurrentTime = CurrentWave.waveDuration;
        _waveActive = true;
        endWave = false;
        startWaveButton.interactable = false;
        _player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
        _spawner.StartSpawning(CurrentWave);
        waveSliderView.SetWaveData(CurrentWave.waveDuration);
        CurentWave = CurrentWave;
    }

    private float timer;

    public void Update()
    {
        if (!_waveActive) return;

        waveSliderView.UpdateSlider(CurrentWave.waveDuration - CurrentTime);
        _spawner.UpdateSpawner(Time.deltaTime);
        if (CurrentTime <= 0f)
        {
            endWave = true;
            _spawner.StopSpawning();
        }
        else
        {
            CurrentTime -= Time.deltaTime;
        }

        if (endWave)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                timer = 0f;
                var enemy = EnemyFactory.Enemies.Find(x => x.IsDeath == false);
                if (enemy == null)
                {
                   EndWave();
                }
            }
        }
        
    }

    public void StopWave()
    {
        playerHealth.gameObject.SetActive(false);
        menuPopUp.alpha = 1f;
        menuPopUp.blocksRaycasts = true;
        startWaveButton.interactable = true;
        endWave = false;
        _waveActive = false;
        _player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Idle);
    }
    

    private void UpdateWave()
    {
        currentWaveIndex++;
        waveWinPs.Play();
        if (currentWaveIndex == levelsData[currentLevelIndex].wavesData.Count)
        {
            gameManager.LevelComplete(levelsData[currentLevelIndex]);
            currentLevelIndex++;
            currentWaveIndex = 0;
            
            if(currentLevelIndex == levelsData.Count)
                gameManager.RestartGame();
        }
        
        waveText.text = (currentWaveIndex).ToString() + "/" + levelsData.Count.ToString();
    }

    private void EndWave()
    {
        UpdateWave();
        StopWave();
    }
}