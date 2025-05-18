using UnityEngine;

public class BoxAmmoController : StaticWeaponController
{
    [SerializeField] private Transform box;
    
    private void Start()
    {
        UnlockCallback += CreateTower;
    }

    private void CreateTower()
    {
        box.gameObject.SetActive(true);        
    }
    protected override void UnLockedUpdate()
    {

    }
}