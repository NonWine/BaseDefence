using TMPro;
using UnityEngine;

public class WeaponUpgradeCardView : CardView
{
    [SerializeField] private TMP_Text levelText;
    
    
    
    public override void SetData(WeaponInfoData weaponInfoData)
    {
        this.weaponInfoData = weaponInfoData;
        levelText.text = weaponInfoData.WeaponUpgradeData.CurrentLevel.ToString();
        title.text = weaponInfoData.WeaponName;
        icon.sprite = weaponInfoData.image;
    }

    public void UpdateData()
    {
        if (weaponInfoData != null)
        {
            levelText.text = weaponInfoData.WeaponUpgradeData.CurrentLevel.ToString();

        }
    }

    protected override void ClickEvent()
    {
        OnClickedWeaponEvent?.Invoke(weaponInfoData);
    }
}