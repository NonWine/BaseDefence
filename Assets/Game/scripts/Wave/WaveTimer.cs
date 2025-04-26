using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    private IEnumerator _timerRoutine;
    private void Awake()
    {
        
    }
    public void StartTimer(int time)
    {
        if(_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
        }
        _timerRoutine = TimerRoutine(time);
        StartCoroutine(_timerRoutine);
    }
    private IEnumerator TimerRoutine(int time)
    {
        for(int i = time; i >= 0; i--)
        {
            _timerText.text = "wave duration: " + i + "s";
            yield return new WaitForSeconds(1f);
        }
    }
}
