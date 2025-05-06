using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardView : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected Button button;
    
    public bool IsSelected { get; private set; }

    public  Action<WeaponInfoData> OnClickedWeaponEvent; 
    
    protected WeaponInfoData weaponInfoData;

    public WeaponInfoData WeaponInfoData => weaponInfoData;
    
    private void Awake()
    {
        button.onClick.AddListener(ClickEvent);
    }

    public abstract void SetData(WeaponInfoData weaponInfoData);

    private void OnDestroy()
    {
        OnClickedWeaponEvent = null;
        button.onClick.RemoveListener(ClickEvent);
    }
    
    protected virtual void ClickEvent()
    {
        IsSelected = true;
        OnClickedWeaponEvent?.Invoke(weaponInfoData);
    }
}