using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour , ITickable
{
    [SerializeField] private PlayerContainer _playerContainer;
    [SerializeField] private HealthUI _healthUI;
    [SerializeField] public Transform bulletStartPoint;
    [SerializeField] private Transform bulletStartParent;
    [Inject] private GameController _gameСontroller;
    [SerializeField] private PlayerGiveDamageHandler _playerGiveDamageHandler;
   [SerializeField] public PlayerCombatManager PlayerCombatManager;
    private PlayerHandlersService _playerHandlersService;
    private OverlapSphereHandler _overlapSphereHandler;
    private PlayerController _playerController;
    private PlayerStateMachine _playerStateMachine;
    private PlayerAnimator _playerAnimator;
    private PlayerRotating _playerRotating;
    public Transform ResourceStartPoint;
    
    
    public PlayerStateMachine PlayerStateMachine => _playerStateMachine;
    

    public PlayerController PlayerController => _playerController;

    public PlayerContainer PlayerContainer => _playerContainer;

    public void Initialize()
    {
        bulletStartPoint.SetParent(bulletStartParent);
        _gameСontroller.RegisterInTick(this);
        InitializeHandler();
        InitializePlayerStateMachine();
        Debug.Log(PlayerCombatManager);

        PlayerInitialize();
        _playerContainer.PlayerStats.CurrentHealth = _playerContainer.PlayerStats.MaxHealth;
        _healthUI.SetHealth(_playerContainer.PlayerStats.MaxHealth);
    }
    
    private void PlayerInitialize()
    {
        _playerController = new PlayerController
        (
            new PlayerMoving(_playerContainer),
            _playerRotating,
            _playerAnimator,
            _playerStateMachine

        );
        
    }
    
    private void InitializePlayerStateMachine()
    {
        _playerStateMachine = new PlayerStateMachine();
        
        var playerStates = new Dictionary<PlayerStateKey, PlayerState>
        {
            { PlayerStateKey.Idle, new PlayerIdleState(_playerStateMachine, _playerContainer) },
            // {
            //     PlayerStateKey.Farming, new FarmingState(_playerStateMachine, _playerContainer,_playerAnimator, _playerHandlersService.DefaultRadiusDamageHandler)
            // },
            //
             {
                PlayerStateKey.Attack, new PlayerAttackState(_playerStateMachine, _playerContainer,_playerAnimator,_playerRotating,  _playerGiveDamageHandler)
            }
        };
        
        _playerStateMachine.RegisterStates(playerStates);
        _playerStateMachine.Initialize(PlayerStateKey.Idle);

    }

    private void InitializeHandler()
    {
        _playerHandlersService = new PlayerHandlersService(_playerContainer, _overlapSphereHandler);
        _playerAnimator = new PlayerAnimator(_playerContainer);
        _playerRotating = new PlayerRotating(_playerContainer);
    }

    public void GetDamage(int dmg)
    {
        Debug.Log("Get Damage");
        _playerContainer.PlayerStats.CurrentHealth -= dmg;
        _healthUI.GetDamageUI(dmg);

        if (_playerContainer.PlayerStats.CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Tick()
    {
      //  _overlapSphereHandler.UpdateOverlapSphere();
        _playerController.Tick();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collided with " + collision.transform.name);
    }
    private void OnDrawGizmos()
    {
        if(_overlapSphereHandler != null)
            _overlapSphereHandler.OnDrawGizmos();
    }

    [Button]
    public void ChangeToAttackState()
    {
        _playerStateMachine.ChangeState(PlayerStateKey.Attack);
    }
    
}