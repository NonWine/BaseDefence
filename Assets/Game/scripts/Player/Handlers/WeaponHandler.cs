[System.Serializable]
public class WeaponHandler
{
    public WeaponInfoData weaponInfoData;
    public bool isLocked = true;
    private float coolDown;
    private int damage;

    public void Init()
    {
        coolDown = weaponInfoData.coolDown;
        damage = weaponInfoData.damage;
    }

    public void UpgradeDamage()
    {
        
    }

    public void UpgradeCoolDown()
    {
        
    }
}