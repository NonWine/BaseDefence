using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseBullet : PoolAble
{
    [SerializeField] private TrailRenderer trailRenderer;
    protected Transform _target;
    //protected Vector3 _target;
    protected Rigidbody rigidbody;
    protected IDamageable _damageable;
    protected Vector3 savedDirection;
    protected bool isAlive;
    protected int _damage;
    protected float timer;
    
    private void Start()
    {
        ResetPool();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name);
        
        
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            
            damageable.GetDamage(_damage);
        }

        isAlive = false;
        DestroyBullet();
    }

    protected virtual void Update()
    {
        if (isAlive)
        {
            var dir = _target.transform.position - transform.position;
            rigidbody.rotation = Quaternion.LookRotation(dir.normalized);
            rigidbody.velocity = dir.normalized * 100f;
            savedDirection = dir;
            
            if (_damageable.IsDeath)
            {
                isAlive = false;
                rigidbody.freezeRotation = true;
                rigidbody.velocity = savedDirection.normalized * 100f;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= 2f)
                DestroyBullet();
        }

    }


    public virtual void Init(int damage, Transform target)
    {
        trailRenderer.Clear();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
        timer = 0f;
        _target = target;
        _damage = damage;
        isAlive = true;
        _damageable = target.GetComponent<IDamageable>();
    }

    protected virtual  void DestroyBullet()
    {
        rigidbody.isKinematic = true;
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
