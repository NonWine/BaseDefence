using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Target : MonoBehaviour, IDamageable
{
    public bool IsDeath { get; private set; }
    [SerializeField] HealthUI _health;
    [SerializeField] int _maxHealth;
    [Inject] private GameManager gameManager;
    public RandomPointInBoxCollider randomPointInBoxCollider;

    private void Awake()
    {
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
        _health.GetDamageUI(damage);
    }
    public void Heal(int value)
    {
        _health.Heal(value);
    }

    private void ResetZabor()
    {
        IsDeath = false;
        gameObject.SetActive(true);
        _health.SetHealth(_maxHealth);

    }

    private void Die()
    {
        IsDeath = true;
        gameObject.SetActive(false);
        gameManager.GameLose();
    }
}
