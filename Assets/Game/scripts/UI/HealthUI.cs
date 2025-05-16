using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float timer;
    [SerializeField] private float timeBeforeStartHeal;
    public bool IsDamaged { get; private set; }

    public event Action OnHPZero;
    public void SetHealth(float value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }
    public void Heal(float value)
    {
        //float curValue = Mathf.FloorToInt(_slider.value);
       if(IsDamaged)
        {
            return;
        }
        float curValue = _slider.value; 
        float finalValue = curValue + value;
        _slider.value = finalValue;
        /*DOVirtual.Float(curValue, finalValue, 0.05f, x =>
        {
            if(IsDamaged)
            {

            }
            _slider.value = x;
        }).SetEase(Ease.Linear).OnKill(() =>
        {
           
        });*/
    }
    public void GetDamageUI(float count)
    {
        //float curValue = Mathf.FloorToInt(_slider.value);
        IsDamaged = true;
        float curValue = _slider.value;
        float finalValue = curValue - count;
        DOVirtual.Float(curValue, finalValue, 0.05f, x =>
            {
                _slider.value = x;
            }).SetEase(Ease.Linear).OnKill(() =>
        {
            _slider.value = finalValue;
            IsDamaged = false;
        });
        if(finalValue <= 0)
        {
            OnHPZero?.Invoke();
        }
    }
}

