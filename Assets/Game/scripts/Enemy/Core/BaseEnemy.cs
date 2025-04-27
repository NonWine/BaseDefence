using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class BaseEnemy : PoolAble , IUnitDamagable , ITickable 
{
    [field: SerializeField] public EnemyStatsConfig EnemyStatsConfig { get; private set; }
    [SerializeField] protected EnemyAnimator EnemyAnimator;
    [SerializeField] protected HealthUI HealthUI;
    [SerializeField] protected NavMeshAgent NavMesh;
    [SerializeField] protected Collider collider;
    [ShowInInspector, ReadOnly] public EnemyStateMachine EnemyStateMachine { get; private set; }
    protected EnemyRotation EnemyRotation;
    
    public event Action<BaseEnemy> OnDie;
    public float CurrentHealth { get; set; }
    public bool IsDeath { get; private set; }

    [field: SerializeField] public Transform Point;
    [Inject] protected Target target;
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
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
    private void Start()
    {
        EnemyStateMachine.ChangeState<MoveState>();
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
            collider.isTrigger = true;
            if(NavMesh.isOnNavMesh)
                NavMesh.isStopped = true;
            NavMesh.velocity = Vector3.zero;
            NavMesh.enabled = false;
            HealthUI.gameObject.SetActive(false);
            EnemyAnimator.SetIdle();
            EnemyAnimator.SetDie();
            EnemyAnimator.Animator.applyRootMotion = true;
            await UniTask.Delay(2500);
            EnemyStateMachine.ChangeState<DieState>();
            OnDie?.Invoke(this);
        }
    }

    protected virtual Dictionary<Type, IEnemyState> CreateStates()
    {
        return new Dictionary<Type, IEnemyState>
        {
            { typeof(IdleState), new IdleState(this, EnemyStateMachine, EnemyAnimator, NavMesh) },
            { typeof(AttackState), new AttackState(this, EnemyStateMachine, new EnemyMelleAttack(this), EnemyAnimator,EnemyRotation, target)},
            { typeof(MoveState), new MoveState(this,EnemyStateMachine, NavMesh, EnemyAnimator, EnemyRotation, target) },
            { typeof(DieState), new DieState(this, EnemyStateMachine) },
            { typeof(ResetingState), new ResetingState(this,EnemyStateMachine, HealthUI, NavMesh, EnemyAnimator) },
        };
    }
    
    public override void ResetPool()
    {
        EnemyStateMachine.ChangeState<ResetingState>();
    }
    public void Attack()
    {
        EnemyStateMachine.ChangeState<AttackState>();
    }

    public void UnsetDeath()
    {
        collider.isTrigger = false;
        IsDeath = false;
    } 
}
