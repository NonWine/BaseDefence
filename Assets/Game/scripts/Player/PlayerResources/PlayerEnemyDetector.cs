public class PlayerEnemyDetector
{
    private PlayerContainer _playerContainer;
    private PlayerStateMachine _playerStateMachine;
    private PlayerAnimator _playerAnimator;
    
    public PlayerEnemyDetector(PlayerContainer playerContainer,
        PlayerStateMachine playerStateMachine, PlayerAnimator playerAnimator)
    {
        _playerContainer = playerContainer;
        _playerAnimator = playerAnimator;
        _playerStateMachine = playerStateMachine;
    }
    

}