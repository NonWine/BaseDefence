using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZaborHealerController : StaticWeaponController
{
    [SerializeField] private ZaborHealer towerExpaPrefab;
    [SerializeField] private Transform towerPoint;
    private void Start()
    {
        UnlockCallback += CreateTower;
    }

    private void CreateTower()
    {
        diContainer.InstantiatePrefabForComponent<ZaborHealer>(towerExpaPrefab, towerPoint.position, Quaternion.identity, null).
            Init(WeaponInfoData);
    }
    protected override void UnLockedUpdate()
    {

    }
}
