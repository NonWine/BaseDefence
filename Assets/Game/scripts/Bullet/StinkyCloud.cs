using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class StinkyCloud : MonoBehaviour
{ 
    [SerializeField] private LayerMask damageableMask;
    private Collider[] _overlapResults = new Collider[20];
     private float _timer;
    private float _existTimer;
    [SerializeField] private ParticleSystem _cloudEffect;
    [Inject] private GameController gameController;
    private Transform parent;
    public DynamicWeapon DynamicWeapon;

    public WeaponUpgradeData WeaponUpgradeData => DynamicWeapon.WeaponUpgradeData;
    
    private void Start()
    {
        ParticlePool.Instance.StinkyBallExplosionFx(transform.position, WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue/ 2f);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void OnEnable()
    {
        Vector3 startScale = new Vector3(WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue * 2, transform.localScale.y, WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue * 2);
        transform.DOScale(startScale, 1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        var main = _cloudEffect.main;
        main.duration = WeaponUpgradeData.GetStat(StatName.Duration).CurrentValue;
        _cloudEffect.Play();
    }
    private void OnDisable()
    {

    }
    private void Damaging()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, WeaponUpgradeData.GetStat(StatName.Radius).CurrentValue, _overlapResults, damageableMask);
        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out BaseEnemy targetDamageable))
            {
                targetDamageable.GetDamage(WeaponUpgradeData.GetStat(StatName.DamageInterval).CurrentValueInt);
            }
        }
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _existTimer += Time.deltaTime;
        if(_timer >= WeaponUpgradeData.GetStat(StatName.DamageInterval).CurrentValue)
        {
            Damaging();
            _timer = 0;
        }
        if(_existTimer >= WeaponUpgradeData.GetStat(StatName.Duration).CurrentValue)
        {
            Destroy(gameObject);
        }
    }
}
