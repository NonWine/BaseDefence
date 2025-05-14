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
    [SerializeField] public GameObject ice;
    [SerializeField] protected EnemyAnimator EnemyAnimator;
    [SerializeField] protected HealthUI HealthUI;
    //[SerializeField] protected Collider collider;
    [SerializeField] protected Collider2D collider;
    [SerializeField] protected Rigidbody2D rigidbody;
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

    public float FreezeTime { get; set; }
    public bool IsDeath { get; private set; }
    public bool IsFreezed { get;  set; }



    public float Speed
    {
        get
        {
            return _speed;
        }
        private set
        {
            _speed = value;
/*            NavMesh.speed = _speed;*/
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
/*            if(NavMesh.isOnNavMesh)
                NavMesh.isStopped = true;
            NavMesh.velocity = Vector3.zero;
            NavMesh.enabled = false;*/
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
            { typeof(IdleState), new IdleState(this, EnemyStateMachine, EnemyAnimator) },
            { typeof(AttackState), new AttackState(this, EnemyStateMachine, new EnemyMelleAttack(this), EnemyAnimator,EnemyRotation, target)},
            { typeof(MoveState), new MoveState(this,EnemyStateMachine, EnemyAnimator, EnemyRotation, target, new MelleMove(this, rigidbody)) },
            { typeof(DieState), new DieState(this, EnemyStateMachine, resourcePartObjFactory) },
            { typeof(ResetingState), new ResetingState(this,EnemyStateMachine, HealthUI, EnemyAnimator) },
            { typeof(FreezedState), new FreezedState(this, EnemyStateMachine, EnemyAnimator) }
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
    public void GetFreezed(float time)
    {
        if(IsFreezed)
        {
            return;
        }
        IsFreezed = true;
        FreezeTime = time;
        EnemyStateMachine.ChangeState<FreezedState>();
    }
}
