using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class DieState : EnemyBaseState
{
    private ResourcePartObjFactory resourcePartObjFactory;
    
    public DieState(BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine, ResourcePartObjFactory resourcePartObjFactory) : base(baseEnemy,  enemyStateMachine)
    {
        this.resourcePartObjFactory = resourcePartObjFactory;
    }
    
    public override void EnterState(BaseEnemy enemy)
    {
        
        ToPool();
    }

    public override void UpdateState()
    {
    }
    

    public override void ExitState()
    {
    }

    private async void ToPool()
    {
        await UniTask.Delay(2500);
        BaseEnemy.OnDie?.Invoke(BaseEnemy);
        BaseEnemy.gameObject.SetActive(false);
    }
    
}