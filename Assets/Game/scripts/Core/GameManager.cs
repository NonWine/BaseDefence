using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour
{
    public const int CardLevelMax = 10;
    
    [SerializeField] private ParticleSystem _confettiFx;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private CanvasGroup _losePanel;
    [SerializeField] private PlayerLevelCompleteView _winPanel;
    [SerializeField] private WaveLevelCompleteView waveLevelCompleteView;
    [SerializeField] private Button restartWaveButton;
    [ProgressBar(0, 10)] [SerializeField] public int TimeGameSpeed = 1;
    public Transform BombTarget;
    private WeaponsSaver weaponsSaver;
    
    private bool isFinish;

    public event Action OnLevelCompleteEvent;

    public event Action OnRestartWaveEvent;

    public event Action OnLooseEvent;
    
    
    private void Awake()
    {
        weaponsSaver = GetComponent<WeaponsSaver>();
        restartWaveButton.onClick.AddListener(RestartWave);
        Application.targetFrameRate = 60;
        Physics.autoSyncTransforms = true;
        Physics.SyncTransforms();
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _gamePanel.SetActive(true);
    }

    private void Update()
    {
        Time.timeScale = TimeGameSpeed;
    }

    private void OnDestroy()
    {
        restartWaveButton.onClick.RemoveListener(RestartWave);

    }

    private void OnApplicationQuit()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
    
    [Button]
    public void GameLose()
    {
        _losePanel.alpha =1f;
        _losePanel.blocksRaycasts = true;
        OnLooseEvent?.Invoke();
    }
    private void RestartWave()
    {
        _losePanel.alpha = 0f;
        _losePanel.blocksRaycasts = false;
        OnRestartWaveEvent?.Invoke();
    }
    
    [Button]
    public async void GameWin()
    {
       
        if (isFinish)
            return;
        isFinish = true;
        _gamePanel.SetActive(false);
        PlayConfetti();
        OnLevelCompleteEvent?.Invoke();
        await UniTask.Delay(2500);
    }

    [Button]
    public void RestartGame()
    {
        isFinish = false;
        _gamePanel.SetActive(true);
        weaponsSaver.ResetWeaponSaves();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    [Button]
    public void LevelComplete(LevelData levelData)
    {
        waveLevelCompleteView.Show(levelData);
    }

    private void PlayConfetti()
    {
        _confettiFx.Play();
    }
}