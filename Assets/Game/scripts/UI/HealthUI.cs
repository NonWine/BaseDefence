using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public event Action OnHPZero;
    public void SetHealth(float value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }
    public void Heal(int value)
    {
        int curValue = Mathf.FloorToInt(_slider.value);
        int finalValue = curValue + value;
        DOVirtual.Int(curValue, finalValue, 0.05f, x =>
        {
            _slider.value = x;
        }).SetEase(Ease.Linear).OnKill(() =>
        {
            _slider.value = finalValue;
        });
    }
    public void GetDamageUI(int count)
    {
        int curValue = Mathf.FloorToInt(_slider.value);
        int finalValue = curValue - count;
        DOVirtual.Int(curValue, finalValue, 0.05f, x =>
            {
                _slider.value = x;
            }).SetEase(Ease.Linear).OnKill(() =>
        {
            _slider.value = finalValue;
        });
        if(finalValue <= 0)
        {
            OnHPZero?.Invoke();
        }
    }
}

