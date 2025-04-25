using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseBullet : PoolAble
{
    protected int _damage;
    protected Transform _target;
    protected Rigidbody rigidbody;
    protected bool isAlive;
    
    private void Start()
    {
        ResetPool();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.transform.TryGetComponent(out IDamageable damageable) && isAlive)
        {
            damageable.GetDamage(_damage);
            DestroyBullet();
        }
    }
    


    public virtual void Init(int damage, Transform target)
    {
        isAlive = true;
        rigidbody = GetComponent<Rigidbody>();
        _target = target;
        _damage = damage;
        rigidbody.velocity = (target.transform.position + (Vector3.up) - transform.position).normalized * 100f;
     //   Invoke(nameof(DestroyBullet),5f);
        
    }

    protected virtual void DestroyBullet()
    {
        isAlive = false;
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
