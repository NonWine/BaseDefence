using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseBullet : PoolAble
{
    private int _damage;
    private Transform _target;
    private float _timer;
    private Rigidbody rigidbody;
    
    private void Start()
    {
        ResetPool();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.GetDamage(_damage);
            DestroyBullet();
        }
    }


    public virtual void Init(int damage, Transform target)
    {
        rigidbody = GetComponent<Rigidbody>();
        _target = target;
        _damage = damage;
        rigidbody.velocity = (target.transform.position + (Vector3.up) - transform.position).normalized * 100f;
        Invoke(nameof(DestroyBullet),5f);
        
    }

    protected virtual void DestroyBullet()
    {
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
