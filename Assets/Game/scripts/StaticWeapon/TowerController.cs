using UnityEngine;
using Zenject;

public class TowerController : StaticWeaponController
{
    [SerializeField] private TowerDefence towerDefencePrefab;
    [SerializeField] private Transform towerPoint;
    
    private void Start()
    {
        UnlockCallback += CreateTower;
    }

    private void CreateTower()
    {
       diContainer.InstantiatePrefabForComponent<TowerDefence>(towerDefencePrefab, towerPoint.position, Quaternion.identity, null);
    }

    protected override void UnLockedUpdate()
    {
        
    }
}