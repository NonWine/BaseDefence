﻿using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerContainer : MonoBehaviour
{
    //[SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _body;
    [SerializeField] private Animator _player;
    private PlayerStats _playerStats;
    [SerializeField] private PlayerAnimatorEvent _playerAnimatorEvent;
    [Inject] private Joystick _joystick;
    
    //[field: SerializeField] public PlayerTrigger PlayerTrigger { get; private set; }
    
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        _playerStats = new PlayerStats();
    }

    public PlayerStateMachine PlayerStateMachine
    {
        get;
        private set;
    }
    
    private Vector3 _direction;

    public Vector3 Direction
    {
        get;
        set;
    }
    
    public float Magmitude
    {
        get;
        set;
    }

    
    //public NavMeshAgent Agent => _navMeshAgent;
 
    public Transform Body => _body;

    public Joystick Joystick => _joystick;

    public Animator Animator => _player;

    public PlayerStats PlayerStats => _playerStats;

    public PlayerAnimatorEvent PlayerAnimatorEvent => _playerAnimatorEvent;
}