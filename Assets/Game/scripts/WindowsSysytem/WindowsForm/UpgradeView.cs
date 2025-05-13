using System;
using DG.Tweening;
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
    [SerializeField] private TMP_Text pricePrintUpgrade;
    [SerializeField] private Button buttonClose;
    [SerializeField] private GameObject statsTitleView;
    [SerializeField] private GameObject levelMaxView;
    [Header("Price Config")]
    [SerializeField] private float BaseValue;
    [SerializeField] private float Modificator;
    
    [SerializeField] private float PrintBaseValue;
    [SerializeField] private float PtintModificator;    
    [Inject] private CollectableManager collectableManager;
    private WeaponInfoData weaponInfoData;
    private int currentPrice;
    private int currentPricePrint;

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
        canvasGroup.DOFade(1f, 0.35f);
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
        button.interactable = true;
        icon.sprite = weaponInfoData.image;
        title.text = weaponInfoData.WeaponName;
        button.onClick.AddListener(TryUpgrade);
        levelTextCurrent.text = "Level " + upgradeData.CurrentLevel.ToString();
        levelTextNext.text = "Level " + (upgradeData.CurrentLevel + 1).ToString();
        statNameText.text = upgradeData.CurrentUpgradeStat().ToString();
        UpgradeValue(upgradeData.CurrentLevel);
        CheckEnoughMoney();
        Show();
        
    }

    private void TryUpgrade()
    {
        var curAmountCoin = collectableManager.GetWallet(eCollectable.coin);
        var curPrints = collectableManager.GetWallet(eCollectable.print);
        if (curAmountCoin.Amount >= currentPrice && curPrints.Amount >= currentPricePrint)
        {
            curAmountCoin.TryRemove(currentPrice);
            curPrints.TryRemove(currentPricePrint);
            
            weaponInfoData.WeaponUpgradeData.Upgrade();
            OnUpgradedEvent?.Invoke();
            if (weaponInfoData.WeaponUpgradeData.IsLevelMax)
            {
                button.interactable = false;
                button.gameObject.SetActive(false);
                statsTitleView.SetActive(false);
                levelMaxView.gameObject.SetActive(true);
            }
            else
            {
                var upgradeData = weaponInfoData.WeaponUpgradeData;
                UpgradeValue(weaponInfoData.WeaponUpgradeData.CurrentLevel);
                levelTextCurrent.text = "Level " + upgradeData.CurrentLevel.ToString();
                levelTextNext.text = "Level " + (upgradeData.CurrentLevel + 1).ToString();
                
                CheckEnoughMoney();

                
            }
        }

    }

    private void CheckEnoughMoney()
    {
        var curAmountCoin = collectableManager.GetWallet(eCollectable.coin);
        var curPrints = collectableManager.GetWallet(eCollectable.print);
        if (curAmountCoin.Amount <= currentPrice || curPrints.Amount <= currentPricePrint)
            button.interactable = false;
    }
    
    
    public virtual void UpgradeValue(int level)
    {
        var value = BaseValue * (1 + Modificator * level);
        currentPrice = Mathf.FloorToInt(value);
        priceUpgrade.text = currentPrice.ToString();
        
         value = PrintBaseValue * (1 + PtintModificator * level);
        currentPricePrint = Mathf.FloorToInt(value);
        pricePrintUpgrade.text = currentPricePrint.ToString();
    }
    
}