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
    [SerializeField] protected float _poisonedSpeedMultiplier;
    [SerializeField] protected float _speed;
    private bool isPoisoned = false;
    
    
    
    [ShowInInspector, ReadOnly] public EnemyStateMachine EnemyStateMachine { get; private set; }
    [Inject] protected PlayerLevelController playerLevelController;
    [Inject] protected ResourcePartObjFactory resourcePartObjFactory;
    [Inject] protected Target target;
    public int CurrentDamage { get;  set; }
    protected EnemyRotation EnemyRotation;
    
    public  Action<BaseEnemy> OnDie;
    public float CurrentHealth { get; set; }
    public bool IsDeath { get; private set; }

    public float Speed
    {
        get
        {
            return _speed;
        }
        private set
        {
            _speed = value;
            NavMesh.speed = _speed;
        }
    }

    public bool IsPoisoned
    {
        get { return isPoisoned; }
        set
        {
            if(value == isPoisoned)
            {
                return;
            }
            isPoisoned = value;
            if(isPoisoned)
            {
                Speed /= _poisonedSpeedMultiplier;
            }
            else
            {
                Speed *= _poisonedSpeedMultiplier;
            }
        }
    }

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
            playerLevelController.AddExperience(EnemyStatsConfig.EXPDrop);
            EnemyStateMachine.ChangeState<DieState>();
        }
    }

    protected virtual Dictionary<Type, IEnemyState> CreateStates()
    {
        return new Dictionary<Type, IEnemyState>
        {
            { typeof(IdleState), new IdleState(this, EnemyStateMachine, EnemyAnimator, NavMesh) },
            { typeof(AttackState), new AttackState(this, EnemyStateMachine, new EnemyMelleAttack(this), EnemyAnimator,EnemyRotation, target)},
            { typeof(MoveState), new MoveState(this,EnemyStateMachine, NavMesh, EnemyAnimator, EnemyRotation, target, new MelleMove(this, NavMesh)) },
            { typeof(DieState), new DieState(this, EnemyStateMachine, resourcePartObjFactory) },
            { typeof(ResetingState), new ResetingState(this,EnemyStateMachine, HealthUI, NavMesh, EnemyAnimator) },
        };
    }
    
    public override void ResetPool()
    {
        EnemyStateMachine.ChangeState<ResetingState>();
    }

    public void UnsetDeath()
    {
        collider.isTrigger = false;
        IsDeath = false;
        IsPoisoned = false;
    } 
}
