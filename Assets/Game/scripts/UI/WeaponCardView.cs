using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WeaponCardView : CardView
{
    [SerializeField] private CardSelectionView cardSelectionView;
    [SerializeField] protected TMP_Text description;



    public override void SetData(WeaponInfoData weaponInfoData)
    {
        this.weaponInfoData = weaponInfoData;
        icon.sprite = weaponInfoData.image;
        title.text = this.weaponInfoData.WeaponName;
        description.text = this.weaponInfoData.description;
    }

    public void SetData(WeaponInfoData weaponInfoData, CardUpgradeInfo cardUpgradeInfo)
    {
        SetData(weaponInfoData);
        description.text = cardUpgradeInfo.Description;

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
        base.ClickEvent();
        cardSelectionView.Select();
    }
}