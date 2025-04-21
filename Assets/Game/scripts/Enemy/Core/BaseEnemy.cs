using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class BaseEnemy : PoolAble , IUnitDamagable , ITickable 
{
    [field: SerializeField] public EnemyStatsConfig EnemyStatsConfig { get; private set; }
    [SerializeField] protected EnemyAnimator EnemyAnimator;
    [SerializeField] protected HealthUI HealthUI;
    [SerializeField] protected NavMeshAgent NavMesh;
    protected EnemyStateMachine EnemyStateMachine;
    protected EnemyRotation EnemyRotation;
    
    public event Action<BaseEnemy> OnDie;
    public float CurrentHealth { get; set; }
    public bool IsDeath { get; private set; }

    [field: SerializeField] public Transform Point;

    public int CurrentDamage { get;  set; }

    private void Awake()
    {
        EnemyRotation = new EnemyRotation(this);
        CurrentHealth = EnemyStatsConfig.MaxHealth;
        CurrentDamage = EnemyStatsConfig.Damage;
        HealthUI.SetHealth(CurrentHealth);
        EnemyStateMachine = new EnemyStateMachine(this);
        EnemyStateMachine.RegisterStates(CreateStates());
        EnemyStateMachine.Initialize<IdleState>();
    }

    public void Tick()
    {
        if(IsDeath)
            return;
        EnemyStateMachine.Update();
    }
    


    public virtual async void GetDamage(int damage)
    {
        if (IsDeath)
            return;
        HealthUI.GetDamageUI(damage);
        CurrentHealth -= damage;
        ParticlePool.Instance.PlayBlood(transform.position);
        if (CurrentHealth <= 0f)
        {
            IsDeath = true;
            NavMesh.isStopped = true;
            await UniTask.Delay(200);
            EnemyStateMachine.ChangeState<DieState>();
            OnDie?.Invoke(this);
        }
    }

    protected virtual Dictionary<Type, IEnemyState> CreateStates()
    {
        return new Dictionary<Type, IEnemyState>
        {
            { typeof(IdleState), new IdleState(this, EnemyStateMachine, EnemyAnimator, NavMesh) },
            { typeof(AttackState), new AttackState(this, EnemyStateMachine, new EnemyMelleAttack(this), EnemyAnimator,EnemyRotation)},
            { typeof(MoveState), new MoveState(this,EnemyStateMachine, NavMesh, EnemyAnimator, EnemyRotation) },
            { typeof(DieState), new DieState(this, EnemyStateMachine) },
            { typeof(ResetingState), new ResetingState(this,EnemyStateMachine, HealthUI) },
        };
    }
    
    public override void ResetPool()
    {
        EnemyStateMachine.ChangeState<ResetingState>();
    }

    public void UnsetDeath() => IsDeath = false;
}
