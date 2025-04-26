using UnityEngine;

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
        Instantiate(towerDefencePrefab, towerPoint.position, Quaternion.identity);
    }

    protected override void UnLockedUpdate()
    {
        
    }
}