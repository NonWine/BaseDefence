using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MiniBomb : MonoBehaviour
{
    [SerializeField] GameObject bombObj;
    [SerializeField] float time;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask damageableMask;
    private Collider2D[] _overlapResults = new Collider2D[20];
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!bombObj.activeInHierarchy)
        {
            return;
        }
        
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, radius, _overlapResults,damageableMask);
        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
            {
                targetDamageable.GetDamage(damage);
            }
        }
        ParticlePool.Instance.PlayExplossion(transform.position, radius);
        collider.enabled = false;
        bombObj.SetActive(false);
        Invoke("EnablingBomb", time);
    }
    private void EnablingBomb()
    {
        bombObj.SetActive(true);
        bombObj.transform.localScale = Vector3.zero;
        bombObj.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack).OnComplete((() =>
        {
            collider.enabled = true;
        }));
    }

}
