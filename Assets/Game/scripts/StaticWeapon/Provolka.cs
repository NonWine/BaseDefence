using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Provolka : StaticWeaponObj
{
    private List<BaseEnemy> enemiesInCollider;
    private float damageInterval;
    private float timer;
    [SerializeField] private float animationScaleY = 1.2f;
    [SerializeField] private float animationDuration = 0.1f;
    public Transform body;
    private float lastAttackTime = -999f;
    private Tween currentTween;
    
    private void Start()
    {
        enemiesInCollider = new List<BaseEnemy>();
        damageInterval = WeaponUpgradeData.GetStat(StatName.DamageInterval).CurrentValue;
        Debug.Log("damage interval is " + damageInterval);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent<BaseEnemy>(out var enemy))
        {
            if (currentTween != null && currentTween.IsActive())
                currentTween.Kill();
            
            AnimateHit();
            enemiesInCollider.Add(enemy);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<BaseEnemy>(out var enemy))
        {
            enemiesInCollider.Remove(enemy);
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill();
                body.localScale = Vector3.one;
            }
        }
    }
    
    private void AnimateHit()
    {
        body.localScale = new Vector3(1, 1, 1); // Сбросим на всякий случай

        currentTween = body
            .DOScaleY(animationScaleY, damageInterval)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutCubic);
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > damageInterval)
        {
            if (enemiesInCollider.Count>0)
            {          
                for(int i = 0; i < enemiesInCollider.Count; i++)
                {
                    if (!enemiesInCollider[i].gameObject.activeInHierarchy)
                    {
                        enemiesInCollider.RemoveAt(i);
                        continue;
                    }
                    enemiesInCollider[i].GetDamage(WeaponUpgradeData.GetStat(StatName.Damage).CurrentValue);
                }
            }
            timer = 0;
        }
    }
}
