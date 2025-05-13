[System.Serializable]
public class DynamicWeaponHandler : WeaponHandler
{
    public DynamicWeapon weaponInfoData;
    public bool isLocked = true;
    public float CurrentTimer;
    public float perShootTimer;

    public DynamicWeaponHandler(DynamicWeapon dynamicWeapon)
    {
        weaponInfoData = dynamicWeapon;
    }
    
    public int CurrentShoot { get; set; }

}