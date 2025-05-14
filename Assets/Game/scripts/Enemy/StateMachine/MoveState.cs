using UnityEngine;
using UnityEngine.AI;

public class MoveState : EnemyBaseState
{
    private EnemyAnimator _enemyAnimator;
    private EnemyRotation _enemyRotation;
    private float _timer;
    private Target _target;
    private Vector3 _targetDirection;
    private BaseEnemy _enemy;
    private float _speed = 17;
    private float _tempSpeed;
    private IMoveable _moveable;
    
    public MoveState(BaseEnemy enemy, EnemyStateMachine enemyStateMachine,
        EnemyAnimator enemyAnimator, EnemyRotation enemyRotation, Target target, IMoveable moveable) 
        : base(enemy, enemyStateMachine)
    {
        _enemyAnimator = enemyAnimator;
        _enemyRotation = enemyRotation;
        
        _target = target;
        _moveable = moveable;
        _enemy = enemy;
    }

    public override void EnterState(BaseEnemy enemy)
    {
        _enemyAnimator.SetRun();
        _timer = float.MaxValue;
        _targetDirection =(_target.randomPointInBoxCollider.GetRandomPointInBox() - (Vector2)enemy.transform.position).normalized;
    }
    
    public override void UpdateState()
    {
        //_agent.Move(_targetDirection * 20 * Time.deltaTime);
        //_agent.Move(new Vector3(0, 0, -1) * 17 * Time.deltaTime);
/*        if (_enemy.IsFreezed)
        {
            return;
        }*/
        _moveable.Move();
    }

    public override void ExitState()
    {

    }


    
}
