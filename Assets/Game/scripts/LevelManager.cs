using System;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private int testLevelIndex;
    public int VisualCurrentLevel { private set; get; }
    public event Action OnFinishLevel;

    private GameObject level;
    private int startLevel;
    public float timerLevel;
    private int countContinueLevel;
    private void Awake()
    {
      //  DontDestroyOnLoad(this);
        Instance = this;
        VisualCurrentLevel = 0;
        VisualCurrentLevel = PlayerPrefs.GetInt("VisualCurrentLevel",1);
        startLevel = VisualCurrentLevel;
    }

    private void Update()
    {
        timerLevel += Time.deltaTime;
    }

    public void FinishLevel()
    {
        VisualCurrentLevel++;
        PlayerPrefs.SetInt("VisualCurrentLevel", VisualCurrentLevel);
        timerLevel = 0f;
        OnFinishLevel?.Invoke();
    }

    public void LoadLevel()
    {
        
        

    }

    [ContextMenu("SetTestLevel")]
    public void SetTestLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", testLevelIndex);
    }
}