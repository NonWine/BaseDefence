using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthUIPlayer : MonoBehaviour
{
    [SerializeField] private Slider _slider; 
    [SerializeField] private float _regenSpeedPerSecond = 1f;          // Швидкість відновлення (кількість HP за секунду)
    [SerializeField] private float _damageAnimationTime = 0.05f;
    [SerializeField] private float _regenAnimationTime = 0.1f;
    [SerializeField] private ZaborHealerController ZaborHealer;
    [SerializeField] private WaveManager WaveManager;

    public bool IsDamaged { get; private set; }
    public event Action OnHPZero;

    private float _targetHealth;

    private void Start()
    {
        _targetHealth = _slider.value;
    }

    public void SetHealth(float value)
    {
        _slider.maxValue = value;
        _slider.value = value;
        _targetHealth = value;
    }

    public void Heal(float value)
    {
        _targetHealth = Mathf.Min(_targetHealth + value, _slider.maxValue);
    }

    
    public void GetDamageUI(float count)
    {
        float curValue = _slider.value;
        float finalValue = Mathf.Max(curValue - count, 0);

        // Зупиняємо поточну анімацію, якщо вона є
        _slider.DOKill();

        // Запускаємо анімацію урону
        DOVirtual.Float(curValue, finalValue, _damageAnimationTime, x =>
        {
            _slider.value = x;
        }).SetEase(Ease.Linear).OnKill(() =>
        {
            _slider.value = finalValue;

            if (finalValue <= 0)
            {
                OnHPZero?.Invoke();
            }
        });

        // Оновлюємо цільове значення для регенерації
        _targetHealth = finalValue;
    }

    private void Update()
    {
        if(ZaborHealer.IsLocked || WaveManager.IsWaveActive == false) return;
        
        if (_slider.value < _targetHealth)
        {
            _slider.DOKill(); // Зупиняємо попередні анімації
            _slider.DOValue(_targetHealth, _regenAnimationTime).SetEase(Ease.Linear);
        }

        // Постійне відновлення
        if (_targetHealth < _slider.maxValue)
        {
            _targetHealth = Mathf.Min(_targetHealth + ZaborHealer.WeaponInfoData.WeaponUpgradeData.GetStat(StatName.healBonus).CurrentValue * Time.deltaTime, _slider.maxValue);
        }
    }
}