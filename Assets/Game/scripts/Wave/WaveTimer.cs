using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class WaveTimer : MonoBehaviour
{
    private IEnumerator _timerRoutine;
    public event Action OnEndTime;

    public int CurrentTime { get; private set; }
    
    private void Awake()
    {
        
    }
    public void StartTimer(int time)
    {
        CurrentTime = time;
        if(_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
        }
        _timerRoutine = TimerRoutine(time);
        StartCoroutine(_timerRoutine);
    }
    private IEnumerator TimerRoutine(int time)
    {
        float timer = CurrentTime;
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;
            CurrentTime = Mathf.FloorToInt(timer);
            yield return null;
        }

        CurrentTime = 0;

        OnEndTime?.Invoke();
    }
}
