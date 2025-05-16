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
    public void Heal(float value)
    {
        float curValue = Mathf.FloorToInt(_slider.value);
        float finalValue = curValue + value;
        DOVirtual.Float(curValue, finalValue, 0.05f, x =>
        {
            _slider.value = x;
        }).SetEase(Ease.Linear).OnKill(() =>
        {
            _slider.value = finalValue;
        });
    }
    public void GetDamageUI(float count)
    {
        float curValue = Mathf.FloorToInt(_slider.value);
        float finalValue = curValue - count;
        DOVirtual.Float(curValue, finalValue, 0.05f, x =>
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

