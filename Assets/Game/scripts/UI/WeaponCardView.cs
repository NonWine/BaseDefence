using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WeaponCardView : CardView
{
    [SerializeField] private CardSelectionView cardSelectionView;
    [SerializeField] protected TMP_Text description;

    public bool IsSelected { get; private set; }

    public event Action<WeaponInfoData> OnClickedWeaponEvent; 
    
    private WeaponInfoData weaponInfoData;

    public override void SetData(WeaponInfoData weaponInfoData)
    {
        this.weaponInfoData = weaponInfoData;
        icon.sprite = weaponInfoData.image;
        title.text = this.weaponInfoData.WeaponName;
        description.text = this.weaponInfoData.description;
    }

    public void DestroyCard()
    {
        transform.DOScale(0f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        }).SetUpdate(true);;
    }

    protected override void ClickEvent()
    {
        IsSelected = true;
        cardSelectionView.Select();
        OnClickedWeaponEvent?.Invoke(weaponInfoData);
    }
}