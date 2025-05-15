using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig", fileName = "Enemy", order = 0)]
public class EnemyStatsConfig : ScriptableObject
{
    [field: SerializeField] public int Damage { get; set; }
    
    [SerializeField] private int _health;
    [SerializeField] public int _moveSpeed;
    [SerializeField] private int _roateSpeed;
    [field: SerializeField] public int CoinDrop { get; private set; } = 3;
    [field: SerializeField] public int EXPDrop { get; private set; } = 3;

    private int _currentHealth;

    
    
    public int RotateSpeed => _moveSpeed;
    
    public int MaxHealth => _health;
}
