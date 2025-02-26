public interface IEnemyState
{
    void EnterState(BaseEnemy enemy);
    void UpdateState();
    void ExitState();
}

public abstract class EnemyBaseState : IEnemyState
{
    protected BaseEnemy BaseEnemy;
    protected EnemyStateMachine _enemyStateMachine;

    public EnemyBaseState(BaseEnemy baseEnemy, EnemyStateMachine enemyStateMachine)
    {
        BaseEnemy = baseEnemy;
        _enemyStateMachine = enemyStateMachine;
    }

    public abstract void EnterState(BaseEnemy enemy);

    public abstract void UpdateState();


    public abstract void ExitState();
}