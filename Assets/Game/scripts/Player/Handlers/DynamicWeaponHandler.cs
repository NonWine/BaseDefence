[System.Serializable]
public class DynamicWeaponHandler : WeaponHandler
{
    public DynamicWeapon weaponInfoData;
    public bool isLocked = true;
    public float CurrentTimer;
    public bool ShootOnce;

    public DynamicWeaponHandler(DynamicWeapon dynamicWeapon)
    {
        weaponInfoData = dynamicWeapon;
    }
    
    public int CurrentShoot { get; set; }

}