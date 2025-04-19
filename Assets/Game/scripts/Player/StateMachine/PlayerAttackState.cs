using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private PlayerAnimator _playerAnimator;
    private PlayerRotating _playerRotating;
    private IDamageableHandler _damageableHandler;
    private PlayerStats _playerStats => player.PlayerStats;
    private Transform _currentTarget;
    private float _timer;
    
    public PlayerAttackState( PlayerStateMachine stateMachine, PlayerContainer playerContainer
        , PlayerAnimator playerAnimator, PlayerRotating playerRotating,
        IDamageableHandler damageableHandler) : base( stateMachine, playerContainer)
    {
        _playerAnimator = playerAnimator;
        _damageableHandler = damageableHandler;
        _playerRotating = playerRotating;
    }

    public override void Enter()
    {
        _playerAnimator.SetStateBehaviour(1);
        
    }

    public override void LogicUpdate()
    {
        Debug.Log(_currentTarget);
        if(_currentTarget == null)
            return;
        _playerRotating.SetTargetRotate(_currentTarget);
    }
    

 
    public override void Exit()
    {
        _playerRotating.UnLockTarget();
        _playerAnimator.SetStateBehaviour(0);
    }

    public void Dispose()
    {
        
    }
}