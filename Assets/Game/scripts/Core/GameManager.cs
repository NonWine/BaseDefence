using System;
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
    [SerializeField] private GameObject _winPanel;

    
    private bool isFinish;

    public event Action OnLevelCompleteEvent;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
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
    public void GameWin()
    {
       
        if (isFinish)
            return;
        isFinish = true;
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);
        OnLevelCompleteEvent?.Invoke();
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