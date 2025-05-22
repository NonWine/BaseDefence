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
    [ShowInInspector, ReadOnly] public EnemyStateMachine EnemyStateMachine { get; private set; }
    [Inject] protected PlayerLevelController playerLevelController;
    [Inject] protected ResourcePartObjFactory resourcePartObjFactory;
    [Inject] protected Target target;
    protected EnemyRotation EnemyRotation;
    public  Action<BaseEnemy> OnDie;
    private bool isPoisoned = false;
    private bool isReduceSpeed = false;

  [ShowInInspector]  public float CurrentHealth { get; set; }
    public int CurrentDamage { get;  set; }
    public float FreezeTime { get; set; }
    public bool IsDeath { get; private set; }
    public bool IsFreezed { get;  set; }

    public int MaxHealth => EnemyStatsConfig.MaxHealth +
                          (  WaveManager.Instance.CurrentLevel * EnemyStatsConfig.HealthCoeffiecntIncrease);

    public float currentSpeed;


    public float Speed
    {
        get
        {
            return currentSpeed;
        }
         set => currentSpeed = Mathf.FloorToInt( value);
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
    
    public bool IsReduceSpeed
    {
        get { return isReduceSpeed; }
        set
        {
            if(value == isReduceSpeed)
            {
                return;
            }
            isReduceSpeed = value;
            if(isReduceSpeed)
            {
                var percentValue = EnemyStatsConfig._moveSpeed * (EnemyStatsConfig.provolkaSpeedReduce / 100f);
                Speed -= percentValue;
            }
            else
            {   
                var percentValue = EnemyStatsConfig._moveSpeed  * (EnemyStatsConfig.provolkaSpeedReduce / 100f);
                Speed += percentValue;
            }
        }
    }

    private void Awake()
    {
        EnemyRotation = new EnemyRotation(this);
        CurrentHealth = MaxHealth;
        currentSpeed = EnemyStatsConfig._moveSpeed;
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

    public virtual async void GetDamage(float damage)
    {
        if (IsDeath)
            return;
        
        HealthUI.GetDamageUI(damage);
        CurrentHealth -= damage;
        ParticlePool.Instance.PlayBlood(transform.position);
        if (CurrentHealth <= 0f)
        {
            IsDeath = true;
            collider.enabled = false;
            HealthUI.gameObject.SetActive(false);
            EnemyAnimator.SetDie();
            playerLevelController.AddExperience(EnemyStatsConfig.EXPDrop);
            EnemyStateMachine.ChangeState<DieState>();
        }
    }

    public void ForceDeath()
    {
        
        IsDeath = true;
        collider.enabled = false;
        HealthUI.gameObject.SetActive(false);
        EnemyAnimator.SetDie();
        EnemyStateMachine.ChangeState<DieState>();
    }

    protected virtual Dictionary<Type, IEnemyState> CreateStates()
    {
        return new Dictionary<Type, IEnemyState>
        {
            { typeof(IdleState), new IdleState(this, EnemyStateMachine, EnemyAnimator) },
            { typeof(AttackState), new AttackState(this, EnemyStateMachine, new EnemyMelleAttack(this), EnemyAnimator,EnemyRotation, target)},
            { typeof(MoveState), new MoveState(this,EnemyStateMachine, EnemyAnimator, EnemyRotation, target, new MelleMove(this, rigidbody)) },
            { typeof(DieState), new DieState(this, EnemyStateMachine, resourcePartObjFactory, EnemyAnimator) },
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
        collider.enabled = true;
        IsDeath = false;
        IsPoisoned = false;
        isReduceSpeed = false;
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
