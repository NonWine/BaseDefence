using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public abstract class Building<TConfig> :   MonoBehaviour, IBuildingDamagable 
where TConfig : BuildingData
{
    [SerializeField] protected TConfig _buildingData;
    [SerializeField] protected HealthUI HealthUI;

    [field: SerializeField]   public Team Team { get; private set; }

    private int _CurrentHealth;


    public event Action OnDeath;
    
    protected virtual void Start()
    {
        _CurrentHealth = _buildingData.Health;
        HealthUI.SetHealth(_CurrentHealth);
    }
    

    public virtual void GetDamage(int damage)
    {
        _CurrentHealth -= damage;
        HealthUI.GetDamageUI(damage);
        ParticlePool.Instance.PlaySparkle(transform.position);
        if (_CurrentHealth <= 0)
        {
            Death();
        }
    }

    public bool IsDeath { get; private set; }

    protected virtual void Death()
    {
        OnDeath?.Invoke();
        IsDeath = true;
        gameObject.SetActive(false);
    }
    
    protected Vector3 GetRandomPointInSphere(SphereCollider sphere)
    {
        Vector3 randomDirection = Random.insideUnitSphere; 
        float randomDistance = Random.Range(0f, sphere.radius); 
        Vector3 localPoint = randomDirection * randomDistance;
        return sphere.transform.position + localPoint; 
    }

}