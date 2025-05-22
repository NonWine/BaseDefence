using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class StinkyCloud : MonoBehaviour
{ 
    [SerializeField] private LayerMask damageableMask;
    private Collider2D[] _overlapResults = new Collider2D[20];
     private float _timer;
    private float _existTimer;
    [SerializeField] private ParticleSystem _cloudEffect;
    [Inject] private GameController gameController;
    private Transform parent;
    private WeaponInfoData WeaponInfoData;
    private WeaponUpgradeData weaponUpgradeData => WeaponInfoData.WeaponUpgradeData;
    public GameObject testCircle;

    //public WeaponUpgradeData WeaponUpgradeData => DynamicWeapon.WeaponUpgradeData;
    
    private void Start()
    {
        
        ParticlePool.Instance.StinkyBallExplosionFx(transform.position, weaponUpgradeData.GetStat(StatName.Radius).CurrentValue/ 2f);
        
        Vector3 startScale = new Vector3(weaponUpgradeData.GetStat(StatName.Radius).CurrentValue * 2,
            weaponUpgradeData.GetStat(StatName.Radius).CurrentValue * 2,
            1f);
        transform.DOScale(startScale, 1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        var main = _cloudEffect.main;
        main.duration = weaponUpgradeData.GetStat(StatName.Duration).CurrentValue;
        _cloudEffect.Play();
    }
    
    public void Init(WeaponInfoData weapon)
    {
        WeaponInfoData = weapon;
    }
    private void Damaging()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position,
            weaponUpgradeData.GetStat(StatName.Radius).CurrentValue, _overlapResults, damageableMask);
        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out BaseEnemy targetDamageable))
            {
                targetDamageable.GetDamage(weaponUpgradeData.GetStat(StatName.Damage).CurrentValueInt);
            }
        }
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _existTimer += Time.deltaTime;
        if(_timer >= weaponUpgradeData.GetStat(StatName.DamageInterval).CurrentValue)
        {
            Damaging();
            _timer = 0;
        }
        if(_existTimer >= weaponUpgradeData.GetStat(StatName.Duration).CurrentValue)
        {
            Destroy(gameObject);
        }
    }
}
