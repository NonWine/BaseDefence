using System;
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
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private LineProgress lineProgress;
    private bool isFinish;
    private bool isTutor;
    private int currentloopLevel;

    public event Action OnLevelCompleteEvent;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        currentloopLevel = PlayerPrefs.GetInt("loopLevel", 1);
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _gamePanel.SetActive(true);
        _levelText.SetText((LevelManager.Instance.VisualCurrentLevel).ToString());
        Time.timeScale = 1f;
       // Bank.Instance.AddCoins(1000000);
        StartLevel(); //StartLevel();
    }

    private void OnApplicationQuit()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
    
    public void GameLose()
    {
        _losePanel.SetActive(true);
        _gamePanel.SetActive(false);
        Debug.Log(lineProgress.GetFillAmount() * 100f);
        Debug.Log(LevelManager.Instance.timerLevel);
        LevelManager.Instance.timerLevel = 0f;
    }
    
    public void GameWin()
    {
       
        if (isFinish)
            return;
        isFinish = true;
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);
        OnLevelCompleteEvent?.Invoke();
        
    }

    public void NextLevel()
    {

        LevelManager.Instance.FinishLevel();
        if (LevelManager.Instance.VisualCurrentLevel == 46)
            EndGame();
        else
        {
          
            isFinish = false;
            _winPanel.SetActive(false);
            _gamePanel.SetActive(true);
            _levelText.SetText((LevelManager.Instance.VisualCurrentLevel).ToString());
            //lineProgress.RestartLineProgress();
            lineProgress.Incoming();
        }
        

    }

    public void EndGame()
    {
        _gamePanel.SetActive(false);
        _winPanel.SetActive(false);
        _endPanel.SetActive(true
            );
    }

    public void StartLevel()
    {
        _levelText.SetText((LevelManager.Instance.VisualCurrentLevel).ToString());
        lineProgress.RestartLineProgress();
        lineProgress.Incoming();
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

    private void ShowUpgradePopUp()
    {
        _upgradePanel.SetActive(true);
        
    } 
        

    public int GetLoopLevel() { return currentloopLevel; }

    public GameObject GetGamePanel() { return _gamePanel; }

    public GameObject GetLosePanel() { return _losePanel; }
}