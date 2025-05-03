using System;
using UnityEngine;
using Zenject;

public class WeaponUpgradeView : Window
{
    [SerializeField] private WeaponUpgradeCardView weaponUpgradeView;
    [SerializeField] private RectTransform container;
    [SerializeField] private UpgradeView upgradeView;
    [Inject] private PlayerCombatManager playerCombatManager;
    [Inject] private StaticWeaponsManager staticWeaponsManager;
    [Inject] private WeaponCardManagerView weaponCardManagerView;
    [Inject] private DiContainer diContainer;

    public override void Initialize(WindowsController windowsController)
    {
        base.Initialize(windowsController);
        weaponCardManagerView.OnGetWeaponEvent += CreateCard;
    }

    private void OnDestroy()
    {
        weaponCardManagerView.OnGetWeaponEvent -= CreateCard;
    }

    private void CreateCard(WeaponInfoData weaponInfoData)
    {
        var card = diContainer.InstantiatePrefabForComponent<WeaponUpgradeCardView>(weaponUpgradeView, container);
        card.SetData(weaponInfoData);
    }
}