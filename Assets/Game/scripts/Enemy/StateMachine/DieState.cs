using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class DieState : EnemyBaseState
{
    private ResourcePartObjFactory resourcePartObjFactory;
    private EnemyAnimator _enemyAnimator;
    public DieState(BaseEnemy baseEnemy,  EnemyStateMachine enemyStateMachine, ResourcePartObjFactory resourcePartObjFactory, 
        EnemyAnimator enemyAnimator) : base(baseEnemy,  enemyStateMachine)
    {
        this.resourcePartObjFactory = resourcePartObjFactory;
        _enemyAnimator = enemyAnimator;
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
        await UniTask.Delay(1500);
        BaseEnemy.OnDie?.Invoke(BaseEnemy);
        Animator animator = _enemyAnimator.Animator;
        animator.CrossFade("New State", 0f);
        animator.Update(0);
        animator.Update(0);

        BaseEnemy.gameObject.SetActive(false);
    }
    
}