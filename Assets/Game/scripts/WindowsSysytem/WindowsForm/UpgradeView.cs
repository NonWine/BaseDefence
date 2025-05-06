using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] protected Image icon;
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected Button button;
    [SerializeField] private TMP_Text levelTextCurrent;
    [SerializeField] private TMP_Text levelTextNext;
    [SerializeField] private TMP_Text statNameText;
    [SerializeField] private TMP_Text priceUpgrade;
    [SerializeField] private Button buttonClose;
    [Header("Price Config")]
    [SerializeField] private float BaseValue;
    [SerializeField] private float Modificator;
    [Inject] private CollectableManager collectableManager;
    private WeaponInfoData weaponInfoData;
    private int currentPrice;

    public event Action OnUpgradedEvent;
    
    private void Awake()
    {
        buttonClose.onClick.AddListener(Hide);
    }

    private void OnDestroy()
    {
        buttonClose.onClick.RemoveListener(Hide);
    }

    private void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    private void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        button.onClick.RemoveAllListeners();
    }

    public void Show(WeaponInfoData weaponInfoData)
    {
 
        this.weaponInfoData = weaponInfoData;
        var upgradeData = weaponInfoData.WeaponUpgradeData;
        icon.sprite = weaponInfoData.image;
        title.text = weaponInfoData.WeaponName;
        button.onClick.AddListener(TryUpgrade);
        levelTextCurrent.text = "Level " + upgradeData.CurrentLevel.ToString();
        levelTextNext.text = "Level " + (upgradeData.CurrentLevel + 1).ToString();
        statNameText.text = upgradeData.CurrentUpgradeStat().ToString();
        UpgradeValue(upgradeData.CurrentLevel);
        Show();
        
    }

    private void TryUpgrade()
    {
        var curAmount = collectableManager.GetWallet(eCollectable.coin);
        
        if (curAmount.Amount >= currentPrice)
        {
            curAmount.TryRemove(currentPrice);
            weaponInfoData.WeaponUpgradeData.Upgrade();
            OnUpgradedEvent?.Invoke();
            if (weaponInfoData.WeaponUpgradeData.LevelMax)
            {
                button.interactable = false;
                priceUpgrade.text = "LEVEL MAX";
            }
            else
            {        
                var upgradeData = weaponInfoData.WeaponUpgradeData;
                UpgradeValue(weaponInfoData.WeaponUpgradeData.CurrentLevel);
                levelTextCurrent.text = "Level " + upgradeData.CurrentLevel.ToString();
                levelTextNext.text = "Level " + (upgradeData.CurrentLevel + 1).ToString();
            }
        }

    }
    
    
    public virtual void UpgradeValue(int level)
    {
        var value = BaseValue * (1 + Modificator * level);
        currentPrice = Mathf.FloorToInt(value);
        priceUpgrade.text = currentPrice.ToString();
    }
    
}