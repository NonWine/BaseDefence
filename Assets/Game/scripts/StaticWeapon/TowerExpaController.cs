using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerExpaController : StaticWeaponController
{
    [SerializeField] private TowerExpa towerExpaPrefab;
    [SerializeField] private Transform towerPoint;
    
    private void Start()
    {
        UnlockCallback += CreateTower;
    }

    private void CreateTower()
    {
        
        diContainer.InstantiatePrefabForComponent<TowerExpa>(towerExpaPrefab, towerPoint.position, Quaternion.identity, null).Init(WeaponInfoData);
    }
    protected override void UnLockedUpdate()
    {

    }
}