using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class WeaponCardManagerView : MonoBehaviour
{
    [Inject] private WeaponInfoData[] allWeapons;
    [Inject] private DiContainer diContainer;
    [SerializeField] private WeaponCardView weaponCardViewPrefab;
    [SerializeField] private RectTransform cardContainer;
    [SerializeField] private CardsUpgradeHandler cardsUpgradeHandler;
    [SerializeField] private float offsetSpeed;
    [SerializeField] private WeaponMergeSystem mergeSystem;
    [SerializeField,ReadOnly] private List<WeaponInfoData> filtredWeapons = new List<WeaponInfoData>();
    private float xOffset;

    [ShowInInspector, ReadOnly]  public List<WeaponCardView> cardViews { get; private set; } = new List<WeaponCardView>() ;

    public event Action<WeaponInfoData> OnGetWeaponEvent;
    
    

    private void Awake()
    {
        allWeapons = allWeapons.ToList().FindAll(x => x is not MergeWeaponData mergeWeaponData).ToArray();
        filtredWeapons = allWeapons.ToList();
    }

    public bool CanCreateCards => filtredWeapons.ToList().Any(x => x.WeaponUpgradeData.IsCardLevelMax == false) || mergeSystem.MergeWeaponsData.Count > 0;

    public void FilterDynamicWeapon()
    {
        var selectedDynamicWeapons = filtredWeapons
            .Where(x => x is DynamicWeapon && x.WeaponUpgradeData.IsUnLocked)
            .ToList();

        filtredWeapons = filtredWeapons
            .Where(x => x is not DynamicWeapon)
            .ToList();

        filtredWeapons.AddRange(selectedDynamicWeapons);
    }

    public void FilterStaticWeapon()
    {
        var selectedDynamicWeapons = filtredWeapons
            .Where(x => x is StaticWeaponData && x.WeaponUpgradeData.IsUnLocked)
            .ToList();

        filtredWeapons = filtredWeapons
            .Where(x =>  x is not StaticWeaponData)
            .ToList();

        filtredWeapons.AddRange(selectedDynamicWeapons);
    }

    public void UnFilterWeapons(WeaponsGeneralType weaponType)
    {
        var selectedDynamicWeapons = allWeapons
            .Where(x => x.WeaponsGeneralType == weaponType)
            .ToList();
        
        filtredWeapons.AddRange(selectedDynamicWeapons);
    }

    private int CountCards()
    {
        var allCount = filtredWeapons.ToList().FindAll(x => x.WeaponUpgradeData.IsCardLevelMax == false);
        allCount.AddRange(mergeSystem.MergeWeaponsData);
        
        if (allCount.Count >= 3)
            return 3;
        return allCount.Count;
    }

    public async void CreateCards(WeaponInfoData[] weaponInfoData = null, bool useStartWeapon = false)
    {
        int count = CountCards();
        WeaponCardView weaponCardView;
        Sequence mainSequence = DOTween.Sequence();
        Ease ease = Ease.OutBack;
        
        if (weaponInfoData != null)
            count = weaponInfoData.Length;
        
        
        weaponInfoData = weaponInfoData ?? allWeapons;
        cardContainer.GetComponent<HorizontalLayoutGroup>().enabled = false;
        xOffset = CardCreatorAnimationConfig.GetXOffset(count, cardContainer.rect.width);
        mainSequence.SetUpdate(true);
        Debug.Log(count);
        
        for (int i = 0; i < count; i++)
        {
            float newOffset;
            
            if (i == 0)
                newOffset = -xOffset;  
            else if (i == 1)
                newOffset = xOffset;  
            else
                newOffset = 0f;

            
            if (useStartWeapon)
            {
                weaponCardView = diContainer.InstantiatePrefabForComponent<WeaponCardView>(weaponCardViewPrefab, cardContainer.transform);
                var weapon = weaponInfoData[i];
                weaponCardView.OnClickedWeaponEvent += GetWeaponFistTime;
                weaponCardView.SetData(weapon);
                cardViews.Add(weaponCardView);

            }
            else
            {
                
                weaponCardView = CreateWeaponCardView();
            }
            
     
            
            RectTransform cardTransform = weaponCardView.GetComponent<RectTransform>();
            cardTransform.anchoredPosition = Vector3.zero;
            cardTransform.pivot = new Vector2(0.5f, 0.5f);
            cardTransform.transform.localScale = Vector3.zero;
            
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            sequence.Append(cardTransform.transform.DOScale(1f, 0.35f).SetEase(ease));
            sequence.AppendInterval(0.15f);
            sequence.Append(cardTransform.DOAnchorPosX(newOffset, offsetSpeed).SetEase(Ease.OutQuart));
            
            float delayBeetWeenCards = 0.25f;
            if (i == 2)
                delayBeetWeenCards = 0.45f;
            
            mainSequence.Insert(i * delayBeetWeenCards, sequence);
            mainSequence.AppendInterval(0.5f * i); 
            //ease = Ease.OutQuart;
        }

        await UniTask.WaitUntil(mainSequence.IsComplete);
        cardContainer.GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    private WeaponCardView CreateWeaponCardView()
    {
        var card = diContainer.InstantiatePrefabForComponent<WeaponCardView>(weaponCardViewPrefab, cardContainer.transform);
        var nonMaxWeapons = filtredWeapons
            .Where(x => !x.WeaponUpgradeData.IsCardLevelMax)  // CardLevelMax == false
            .Where(x => !cardViews.Any(j => j.WeaponInfoData.WeaponName == x.WeaponName))  // Не міститься в cardViews
            .ToList();
        
        
        foreach (var mergeWeaponData in mergeSystem.MergeWeaponsData)
        {
            if(!nonMaxWeapons.Contains(mergeWeaponData))
                nonMaxWeapons.Add(mergeWeaponData);
        }
        var weapon = nonMaxWeapons[Random.Range(0, nonMaxWeapons.Count)];
        
        if (weapon.WeaponUpgradeData.IsUnLocked) 
        {
            card.OnClickedWeaponEvent += GetWeaponAndUpgradeItLevel;
            var upgradeData =   cardsUpgradeHandler.GetUpgradeData(weapon);
            card.SetData(weapon,upgradeData);

        }
        else
        {
            card.OnClickedWeaponEvent += GetWeaponFistTime;
            card.SetData(weapon);
        }
        
        cardViews.Add(card);
        return card;
    }

    

    private void GetWeaponFistTime(WeaponInfoData weaponInfoData)
    {
        UnscribeFromCards();
        mergeSystem.CheckGetMergeWeapon(weaponInfoData);
        weaponInfoData.WeaponUpgradeData.IsUnLocked = true;
        OnGetWeaponEvent?.Invoke(weaponInfoData);
    }

    private void GetWeaponAndUpgradeItLevel(WeaponInfoData weaponInfoData)
    {
        UnscribeFromCards();
        cardsUpgradeHandler.UpgradeCard(weaponInfoData);
        mergeSystem.TryUnlockMergeWeapon();
        OnGetWeaponEvent?.Invoke(weaponInfoData);
    }
    
    private void UnscribeFromCards()
    {
        for (var i = cardViews.Count - 1; i >= 0; i--)
        {
            if (cardViews[i].IsSelected == false)
                cardViews[i].GetComponent<CardSelectionView>().Deselect();

            cardViews[i].OnClickedWeaponEvent -= GetWeaponFistTime;
            cardViews[i].OnClickedWeaponEvent -= GetWeaponAndUpgradeItLevel;

        }
    }

    public void DestroyCards()
    {
        for (var i = cardViews.Count - 1; i >= 0; i--)
        {
           cardViews[i].DestroyCard();
        }
        cardViews.Clear();
    }
}