using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private PlayerAnimator _playerAnimator;
    private PlayerRotating _playerRotating;
    private PlayerGiveDamageHandler _playerGiveDamageHandler;
    
    public PlayerAttackState( PlayerStateMachine stateMachine, PlayerContainer playerContainer
        , PlayerAnimator playerAnimator, PlayerRotating playerRotating, PlayerGiveDamageHandler playerGiveDamageHandler
         ) : base( stateMachine, playerContainer)
    {
        _playerAnimator = playerAnimator;
        _playerRotating = playerRotating;
        _playerGiveDamageHandler = playerGiveDamageHandler;
    }

    public override void Enter()
    {
        _playerAnimator.SetStateBehaviour(1);
        player.PlayerAnimatorEvent.GetComponent<Animator>().SetInteger("state", 1);
        
    }

    public override void LogicUpdate()
    {
        var enemy = _playerGiveDamageHandler.GetNearestEnemy(player.transform, 25);
        if (enemy != null)
        {
            _playerGiveDamageHandler.CurrentAgredTarget = enemy.transform;

            Vector3 direction = _playerGiveDamageHandler.CurrentAgredTarget.position - _playerGiveDamageHandler.transform.position;
            /*            Quaternion LookDirection = Quaternion.LookRotation(direction);
                        transform.rotation = LookDirection;*/
            player.Direction = direction;
        }
        //Debug.Log(_playerGiveDamageHandler);
        _playerGiveDamageHandler.TryGetDamage(player.Player);
        // if(_playerGiveDamageHandler.CurrentAgredTarget != null)
            _playerRotating.SetTargetRotate(_playerGiveDamageHandler.CurrentAgredTarget);
        _playerAnimator.SetStateBehaviour(1);

    }
    

 
    public override void Exit()
    {
        _playerRotating.UnLockTarget();
        _playerAnimator.SetStateBehaviour(0);
    }
    
}