using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confettiFx;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private LevelCompleteView _winPanel;

    
    private bool isFinish;

    public event Action OnLevelCompleteEvent;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Physics.autoSyncTransforms = true;
        Physics.SyncTransforms();
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _gamePanel.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
    
    public void GameLose()
    {
        _losePanel.SetActive(true);
        _gamePanel.SetActive(false);
        Debug.Log(LevelManager.Instance.timerLevel);
        LevelManager.Instance.timerLevel = 0f;
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
        _winPanel.Show();
    }

    public void RestartGame()
    {
        isFinish = false;
        _gamePanel.SetActive(true);
        _losePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PlayConfetti()
    {
        _confettiFx.Play();
    }
}