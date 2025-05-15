using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public abstract class BaseBullet : PoolAble
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private WeaponInfoData WeaponInfoData;
    protected Transform _target;
    //protected Vector3 _target;
    //protected Rigidbody rigidbody;
    protected Rigidbody2D rigidbody;
    protected IDamageable _damageable;
    protected Vector3 savedDirection;
    protected bool isAlive;
    protected float timer;

    protected WeaponUpgradeData WeaponUpgradeData => WeaponInfoData.WeaponUpgradeData;
    
    private void Start()
    {
        ResetPool();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        
        
        if (other.transform.TryGetComponent(out IDamageable damageable))
        {
            
            damageable.GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
        }

        isAlive = false;
        DestroyBullet();
    }

    protected virtual void Update()
    {
        if (isAlive)
        {
            var dir = _target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rigidbody.rotation = angle;
            rigidbody.velocity = dir.normalized * WeaponUpgradeData.GetStat(StatName.ProjectileSpeed).CurrentValue;
            savedDirection = dir;
            
            if (_damageable.IsDeath)
            {
                isAlive = false;
                rigidbody.freezeRotation = true;
                rigidbody.velocity = savedDirection.normalized *  WeaponUpgradeData.GetStat(StatName.ProjectileSpeed).CurrentValue;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= 2f)
                DestroyBullet();
        }

    }


    public virtual void Init(Transform target)
    {
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
        timer = 0f;
        _target = target;
        isAlive = true;
        _damageable = target.GetComponent<IDamageable>();
    }

    protected virtual  void DestroyBullet()
    {
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }
        gameObject.SetActive(false);
    }

    public override void ResetPool()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
        });

    }
}
