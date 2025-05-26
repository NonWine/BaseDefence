using UnityEngine;

public class PlayerIdleState : PlayerState
{
    
    public PlayerIdleState( PlayerStateMachine stateMachine, PlayerContainer playerContainer) : base(stateMachine, playerContainer)
    {
    }

    public override void Enter()
    {
        player.Direction = Vector3.zero;
        player.PlayerAnimatorEvent.GetComponent<Animator>().SetInteger("state", 0);
    }

    public override void LogicUpdate()
    {
        player.Player.PlayerGiveDamageHandler.TryGetDamage(player.Player);
    }

    public override void Exit()
    {
    }
}