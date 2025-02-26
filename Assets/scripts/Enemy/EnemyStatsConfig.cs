using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig", fileName = "Enemy", order = 0)]
public class EnemyStatsConfig : ScriptableObject
{
    [field: SerializeField] public int Damage { get; set; }
    
    [SerializeField] private int _health;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _roateSpeed;
    [field: SerializeField] public float TargetDistance { get; private set; }
    
    private int _currentHealth;

    
    public int MoveSpeed => _moveSpeed;
    
    public int RotateSpeed => _moveSpeed;
    
    public int MaxHealth => _health;
}
