using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WaveSliderView : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public event Action OnUpdateEvent; 
    
    public void SetWaveData(float time)
    {
        slider.value = 0f;
        slider.maxValue = time;
    }

    public void UpdateSlider(float valueTime)
    {
        slider.value = valueTime;
    }
    
}