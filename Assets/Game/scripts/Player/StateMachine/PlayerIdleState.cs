using UnityEngine;
using Zenject;

public class PlayerIdleState : PlayerState
{
    private PlayerGiveDamageHandler _playerHandler;
    public PlayerIdleState( PlayerStateMachine stateMachine, PlayerContainer playerContainer, PlayerGiveDamageHandler playerHandler)
        : base(stateMachine, playerContainer)
    {
        _playerHandler = playerHandler;
    }

    public override void Enter()
    {
        player.PlayerAnimatorEvent.GetComponent<Animator>().SetInteger("state", 0);
        _playerHandler.CurrentAgredTarget = null;
        player.Direction = Vector3.zero;
    }

    public override void LogicUpdate()
    {
    }

    public override void Exit()
    {
    }
}