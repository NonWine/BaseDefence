using DG.Tweening;
using UnityEngine.AI;

public class DieState : EnemyBaseState
{
    
    public DieState(BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine) : base(baseEnemy,  enemyStateMachine)
    {
    }
    
    public override void EnterState(BaseEnemy enemy)
    {
        BaseEnemy.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {
    }
    
    

    public override void ExitState()
    {
    }
}