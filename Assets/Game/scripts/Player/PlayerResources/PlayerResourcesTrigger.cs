using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerResourcesTrigger : PlayerTrigger
{
    
    [SerializeField,ReadOnly] private Player _player;

    private PlayerStateMachine _playerStateMachine;
    public event Action<eCollectable> CurrentResourceTrigger;

    protected  void Awake()
    {
        _player = GetComponentInParent<Player>();
        _playerStateMachine = _player.PlayerStateMachine;
    }
    
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
      

    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}