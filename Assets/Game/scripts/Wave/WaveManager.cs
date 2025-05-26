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
   [SerializeField] private HealthUIPlayer playerHealth;
   [SerializeField] private CanvasGroup menuPopUp;
   [SerializeField] private ParticleSystem waveWinPs;
   [Inject] private EnemyFactory EnemyFactory;
   [Inject] private PlayerHandler _player;
   [Inject] private GameManager gameManager;
   [ReadOnly] public WaveDataConfig CurentWave;
   private int currentLevelIndex;
   private int currentWaveIndex;
   private bool endWave;
   
   public float CurrentTime { get; private set; }
   public bool IsWaveActive { get; private set; }

   public int CurrentLevel => currentLevelIndex;
   
   public WaveDataConfig CurrentWave => levelsData[currentLevelIndex].wavesData[currentWaveIndex];
   
   public event Action OnEndWave;
   
   public event Action OnLevelFinished;

   public static WaveManager Instance { get; private set; }

   private void OnEnable()
    {
        
        gameManager.OnLooseEvent += StopWave;
        startWaveButton.onClick.AddListener(StartWave);
        currentLevelIndex = PlayerPrefs.GetInt(nameof(currentLevelIndex), currentLevelIndex);
        currentWaveIndex = PlayerPrefs.GetInt(nameof(currentWaveIndex), currentWaveIndex);
    }
    private void OnDisable()
    {
        gameManager.OnLooseEvent -= StopWave;
        startWaveButton.onClick.RemoveListener(StartWave);
        PlayerPrefs.SetInt(nameof(currentLevelIndex), currentLevelIndex);
        PlayerPrefs.SetInt(nameof(currentWaveIndex), currentWaveIndex);
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
        IsWaveActive = true;
        endWave = false;
        startWaveButton.interactable = false;
        //_player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Attack);
        
        _spawner.StartSpawning(CurrentWave);
        waveSliderView.SetWaveData(CurrentWave.waveDuration);
        CurentWave = CurrentWave;
    }

    private float timer;

    public void Update()
    {
        if (!IsWaveActive) return;

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
        IsWaveActive = false;
        _player.Player.PlayerStateMachine.ChangeState(PlayerStateKey.Idle);
    }
    

    private void UpdateWave()
    {
        currentWaveIndex++;
        OnEndWave?.Invoke();
        waveWinPs.Play();
        if (currentWaveIndex == levelsData[currentLevelIndex].wavesData.Count)
        {
            gameManager.LevelComplete(levelsData[currentLevelIndex]);
            currentLevelIndex++;
            currentWaveIndex = 0;
            OnLevelFinished?.Invoke();
            if(currentLevelIndex == levelsData.Count)
                gameManager.RestartGame();
        }
        
        waveText.text = (currentWaveIndex ).ToString() + "/" + levelsData[currentLevelIndex].wavesData.Count.ToString();
    }

    private void EndWave()
    {
        UpdateWave();
        StopWave();
    }
}