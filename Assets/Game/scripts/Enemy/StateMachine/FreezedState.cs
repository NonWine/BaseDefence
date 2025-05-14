using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezedState : EnemyBaseState
{

    private EnemyAnimator _enemyAnimator;
    private float freezeTimer;
    public FreezedState(BaseEnemy baseEnemy, EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator):
        base(baseEnemy, enemyStateMachine)
    {
        _enemyAnimator = enemyAnimator;
    }
    public override void EnterState(BaseEnemy enemy)
    {
        freezeTimer = 0;
        BaseEnemy.ice.SetActive(true);
        _enemyAnimator.Freeze();
    }
    public override void UpdateState()
    {
        freezeTimer += Time.deltaTime;
        if(freezeTimer > BaseEnemy.FreezeTime)
        {
            _enemyStateMachine.ChangeState<MoveState>();
        }
    }
    public override void ExitState()
    {
        BaseEnemy.ice.SetActive(false);
        _enemyAnimator.UnFreeze();
        BaseEnemy.IsFreezed = false;
    }
}
