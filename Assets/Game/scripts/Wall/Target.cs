using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public bool IsDeath { get; private set; }
    [SerializeField] HealthUI _health;
    [SerializeField] int _maxHealth;
    public RandomPointInBoxCollider randomPointInBoxCollider;

    private void Awake()
    {
        _health.SetHealth(_maxHealth);
    }
    private void OnEnable()
    {
        _health.OnHPZero += Die;
    }
    private void OnDisable()
    {
        _health.OnHPZero -= Die;
    }
    public void GetDamage(int damage)
    {
        _health.GetDamageUI(damage);
    }


    private void Die()
    {
        IsDeath = true;
        gameObject.SetActive(false);
    }
}
