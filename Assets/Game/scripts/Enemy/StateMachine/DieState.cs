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
        int dropCount = enemy.EnemyStatsConfig.CoinDrop;
        while (dropCount >= 0)
        {
            var coin = resourcePartObjFactory.Create(typeof(Coin));
            coin.transform.position = BaseEnemy.transform.position + ( Random.insideUnitSphere * 1f);
            coin.PickUp();
            dropCount--;
        }
        
        // dropCount = enemy.EnemyStatsConfig.EXPDrop;
        //
        // while (dropCount >= 0)
        // {
        //     var exp = resourcePartObjFactory.Create(typeof(Experience));
        //     exp.PickUp();
        //     dropCount--;
        // }
        
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