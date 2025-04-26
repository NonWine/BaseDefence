using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
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
        for(; CurrentTime >= 0; CurrentTime--)
        {
            _timerText.text = "wave duration: " + CurrentTime + "s";
            yield return new WaitForSeconds(1f);
        }
    }
}
