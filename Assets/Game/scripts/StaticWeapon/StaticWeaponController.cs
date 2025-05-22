using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public abstract class StaticWeaponController : MonoBehaviour , ITickable
{
    [SerializeField, ReadOnly] protected bool isLocked = true;
    [SerializeField] public WeaponInfoData WeaponInfoData;
    [Inject] private GameController gameController;
    [Inject] private WeaponCardManagerView _weaponCardManagerView;
    [Inject] protected DiContainer diContainer;
    
    protected float timer;
    public bool IsLocked => isLocked;
    
    public Action UnlockCallback;
    
    protected virtual void Awake()
    {
        gameController.RegisterInTick(this);
        _weaponCardManagerView.OnGetWeaponEvent += GetWeaponCard;
        Invoke(nameof(TryLoad),0.2f);
    }

    private void TryLoad()
    {
        if (WeaponInfoData.WeaponUpgradeData.IsUnLocked)
        {
            isLocked = false;
            UnlockCallback?.Invoke();
            
        }
    }

    protected virtual void OnDestroy()
    {
        UnlockCallback = null;
        _weaponCardManagerView.OnGetWeaponEvent -= GetWeaponCard;

    }

    public virtual void GetWeaponCard(WeaponInfoData weaponInfoData)
    {
        if (weaponInfoData == WeaponInfoData && isLocked)
        {
             isLocked = false;
             UnlockCallback?.Invoke();
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