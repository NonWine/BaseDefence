using UnityEngine;

public class PlayerIdleState : PlayerState
{
    
    public PlayerIdleState( PlayerStateMachine stateMachine, PlayerContainer playerContainer) : base(stateMachine, playerContainer)
    {
    }

    public override void Enter()
    {
        player.PlayerAnimatorEvent.GetComponent<Animator>().SetInteger("state", 0);
    }

    public override void LogicUpdate()
    {
    }

    public override void Exit()
    {
    }
}