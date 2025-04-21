using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseBullet : PoolAble
{
    private int _damage;
    private Transform _target;
    private float _timer;
    
    private void Start()
    {
        ResetPool();
    }

    private void Update()
    {
        if (_target == null) 
        {
            return;
        }
        
        

        transform.position = Vector3.MoveTowards(transform.position, _target.position + Vector3.up, 10f * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.position) <= 1f)
        {
            if (_target.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage(_damage);
                _target = null;
            }
            gameObject.SetActive(false);
        }
    }
    
 
    public virtual void Init(int damage, Transform target)
    {
        _target = target;
        _damage = damage;
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
