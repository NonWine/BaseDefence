[System.Serializable]
public class WeaponHandler
{
    public DynamicWeapon weaponInfoData;
    public bool isLocked = true;
    private float coolDown;
    private int damage;

    public float CurrentTimer;

    public float CoolDown => coolDown;

    public int Damage => damage;

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