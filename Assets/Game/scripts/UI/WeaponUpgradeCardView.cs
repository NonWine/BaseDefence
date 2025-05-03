using TMPro;
using UnityEngine;

public class WeaponUpgradeCardView : CardView
{
    [SerializeField] private TMP_Text levelText;

    public void Init()
    {
        
    }

    public override void SetData(WeaponInfoData weaponInfoData)
    {
        levelText.text = "1";
        title.text = weaponInfoData.WeaponName;
        icon.sprite = weaponInfoData.image;
    }

    protected override void ClickEvent()
    {
        
    }
}