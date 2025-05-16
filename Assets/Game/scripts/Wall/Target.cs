using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Target : MonoBehaviour, IDamageable
{
    public bool IsDeath { get; private set; }
    [SerializeField] HealthUI _health;
    [SerializeField] int _maxHealth;
    [Inject] private GameManager gameManager;
    public RandomPointInBoxCollider randomPointInBoxCollider;
    private Sequence Sequence;
    public bool IsDamaged => _health.IsDamaged;
    private void Awake()
    {
        Sequence = DOTween.Sequence();
        _health.SetHealth(_maxHealth);
        gameManager.OnRestartWaveEvent += ResetZabor;
    }
    
    private void OnEnable()
    {
        _health.OnHPZero += Die;
        gameManager.OnRestartWaveEvent -= ResetZabor;
        
    }
    private void OnDisable()
    {
        _health.OnHPZero -= Die;
    }
    public void GetDamage(int damage)
    {
        if (Sequence.IsPlaying())
        {
            Sequence.Kill();
            transform.localScale = Vector3.one;
        }
        Sequence.Append(transform.DOScale(1.2f, 0.15f)).SetEase(Ease.OutQuad);
        Sequence.Append(transform.DOScale(1f, 0.15f)).SetEase(Ease.Linear);
        
        _health.GetDamageUI(damage);
    }
    public void Heal(float value)
    {
        _health.Heal(value);
    }

    private void ResetZabor()
    {
        IsDeath = false;
        gameObject.SetActive(true);
        Sequence.Kill();
        transform.localScale = Vector3.one;
        _health.SetHealth(_maxHealth);

    }

    private void Die()
    {
        IsDeath = true;
        gameObject.SetActive(false);
        gameManager.GameLose();
    }
}
