using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponUpgradeView : Window
{
    [SerializeField] private WeaponUpgradeCardView weaponUpgradeView;
    [SerializeField] private RectTransform container;
    [SerializeField] private UpgradeView upgradeView;
    [SerializeField] private Button toMenu;
    [Inject] private WeaponCardManagerView weaponCardManagerView;
    [Inject] private DiContainer diContainer;
    [Inject] private WeaponInfoData[] allWeapons;

    private List<WeaponUpgradeCardView> weaponUpgradeCardViews = new List<WeaponUpgradeCardView>();

    private void ToMenu() => WindowsController.ToWave();

    public override void Initialize(WindowsController windowsController)
    {
        base.Initialize(windowsController);
        toMenu.onClick.AddListener(ToMenu);
        upgradeView.OnUpgradedEvent += UpdateAllCards;
        weaponCardManagerView.OnGetWeaponEvent += CreateCard;
        
        
        foreach (var weaponInfoData in allWeapons)
        {
            if (weaponInfoData.WeaponUpgradeData.CurrentLevel > 0)
            {
                CreateCard(weaponInfoData);
            }
        }
    }

    private void OnEnable()
    {
        UpdateAllCards();
    }

    private void OnDestroy()
    {
        weaponCardManagerView.OnGetWeaponEvent -= CreateCard;
        toMenu.onClick.RemoveListener(ToMenu);
        upgradeView.OnUpgradedEvent -= UpdateAllCards;

    }

    private void CreateCard(WeaponInfoData weaponInfoData)
    {
        if (CheckCopyCard(weaponInfoData)) return;

        var card = diContainer.InstantiatePrefabForComponent<WeaponUpgradeCardView>(weaponUpgradeView, container);
        card.SetData(weaponInfoData);
        weaponUpgradeCardViews.Add(card);
        card.OnClickedWeaponEvent = ShowUpgradeView;
    }

    private void UpdateAllCards()
    {
        weaponUpgradeCardViews.ForEach(x=> x.UpdateData());
    }
    

    private void ShowUpgradeView(WeaponInfoData weaponInfoData)
    {
        if(weaponInfoData.WeaponUpgradeData.LevelMax) return;
        
        upgradeView.Show(weaponInfoData);
    }

    private bool CheckCopyCard(WeaponInfoData weaponInfoData)
    {
        foreach (var weaponUpgradeCardView in weaponUpgradeCardViews)
        {
            if (weaponUpgradeCardView.WeaponInfoData == weaponInfoData)
            {
                return true;
            }
        }

        return false;
    }
}