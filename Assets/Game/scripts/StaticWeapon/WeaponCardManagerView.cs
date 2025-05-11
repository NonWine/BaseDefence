using System;
using System.Collections.Generic;
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
    private float xOffset;
    
    [ShowInInspector, ReadOnly]  public List<WeaponCardView> cardViews { get; private set; } = new List<WeaponCardView>() ;

    public event Action<WeaponInfoData> OnGetWeaponEvent;
    // costil
    private int j = 0;
    //
    

    public async void CreateCards()
    {
        cardContainer.GetComponent<HorizontalLayoutGroup>().enabled = false;
        
        Ease ease = Ease.OutBack;
        xOffset = cardContainer.rect.width / 3f;
        Sequence mainSequence = DOTween.Sequence();
        mainSequence.SetUpdate(true);
        
        for (int i = 0; i < 3; i++)
        {
            float newOffset;
            
            if (i == 0)
                newOffset = -xOffset;  
            else if (i == 1)
                newOffset = xOffset;  
            else
                newOffset = 0f;

            var card = CreateWeaponCardView();
            j++;
            if (j == allWeapons.Length)
                j = 0;
     
            
            RectTransform cardTransform = card.GetComponent<RectTransform>();
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
        cardViews.Add(card);
        
        var weapon = allWeapons[j];
        if (allWeapons[j].WeaponUpgradeData.CardLevelMax)
        {
            Debug.Log("CArd Level MAx");
        }
        
        if (weapon.WeaponUpgradeData.IsUnLocked && weapon is DynamicWeapon dynamicWeapon) 
        {
            card.OnClickedWeaponEvent += GetWeaponAndUpgradeItLevel;
            var upgradeData =   cardsUpgradeHandler.GetUpgradeData(weapon);
            card.SetData(allWeapons[j],upgradeData);

        }
        else
        {
            card.OnClickedWeaponEvent += GetWeaponFistTime;
            card.SetData(allWeapons[j]);
        }

        return card;
    }

    private void GetWeaponFistTime(WeaponInfoData weaponInfoData)
    {
        UnscribeFromCards();
        weaponInfoData.WeaponUpgradeData.IsUnLocked = true;
        OnGetWeaponEvent?.Invoke(weaponInfoData);
    }

    private void GetWeaponAndUpgradeItLevel(WeaponInfoData weaponInfoData)
    {
        UnscribeFromCards();
        cardsUpgradeHandler.UpgradeCard(weaponInfoData);
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
