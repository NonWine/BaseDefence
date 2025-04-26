using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public abstract class StaticWeaponController : MonoBehaviour , ITickable
{
    [field: SerializeField] public WeaponType WeaponType;
    [SerializeField, ReadOnly] private bool isLocked = true;
    [Inject] private GameController gameController;
    [Inject] private WeaponManagerView weaponManagerView;
    protected float timer;
    
    public Action UnlockCallback;
    
    protected virtual void Awake()
    {
        gameController.RegisterInTick(this);
        weaponManagerView.OnGetWeaponEvent += GetWeapon;
    }

    protected virtual void OnDestroy()
    {
        UnlockCallback = null;
        weaponManagerView.OnGetWeaponEvent -= GetWeapon;

    }

    public virtual void GetWeapon(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData is StaticWeaponData staticWeaponData)
        {
            if (WeaponType == staticWeaponData.WeaponType)
            {
                isLocked = false;
                UnlockCallback?.Invoke();
            }
        }
    }

    public  void Tick()
    {
        if (!isLocked)
        {
            UnLockedUpdate();
        }        
    }

    protected abstract void UnLockedUpdate();
}