using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponManagerView : MonoBehaviour
{
    [SerializeField] private WeaponInfoData[] allWeapons;
    [SerializeField] private CardView cardViewPrefab;
    [SerializeField] private RectTransform cardContainer;
    [Inject] private DiContainer diContainer;
    [SerializeField] private float offsetSpeed;
    private float xOffset;
    
    [ShowInInspector, ReadOnly]  public List<CardView> cardViews { get; private set; } = new List<CardView>() ;

    public event Action<WeaponInfoData> OnGetWeaponEvent;
    

    public async void CreateCards()
    {
        cardContainer.GetComponent<HorizontalLayoutGroup>().enabled = false;
        
        Ease ease = Ease.OutBack;
        xOffset = cardContainer.rect.width / 3f;
        Sequence mainSequence = DOTween.Sequence();
        for (int i = 0; i < 3; i++)
        {
            float newOffset;
            
            if (i == 0)
                newOffset = -xOffset;  
            else if (i == 1)
                newOffset = xOffset;  
            else
                newOffset = 0f;       
            
            var card = diContainer.InstantiatePrefabForComponent<CardView>(cardViewPrefab,cardContainer.transform);
            card.SetData(allWeapons[i]);
            card.OnClickedWeaponEvent += GetWeapon;
            cardViews.Add(card);
            
            RectTransform cardTransform = card.GetComponent<RectTransform>();
            cardTransform.anchoredPosition = Vector3.zero;
            cardTransform.pivot = new Vector2(0.5f, 0.5f);
            cardTransform.transform.localScale = Vector3.zero;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(cardTransform.transform.DOScale(1f, 0.35f).SetEase(ease));
            sequence.AppendInterval(0.15f);
            sequence.Append(cardTransform.DOAnchorPosX(newOffset, offsetSpeed).SetEase(Ease.OutQuart));
            
            float delayBeetWeenCards = 0.25f;
            if (i == 2)
                delayBeetWeenCards = 0.45f;
            
            mainSequence.Insert(i * delayBeetWeenCards, sequence);
            mainSequence.AppendInterval(0.5f); 
            //ease = Ease.OutQuart;
        }

        await UniTask.WaitUntil(mainSequence.IsComplete);
        cardContainer.GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    private void GetWeapon(WeaponInfoData weaponInfoData)
    {
        for (var i = cardViews.Count - 1; i >= 0; i--)
        {
            if(cardViews[i].IsSelected == false)
                cardViews[i].GetComponent<CardSelectionView>().Deselect();
            
            cardViews[i].OnClickedWeaponEvent -= GetWeapon; 
        }
        
        OnGetWeaponEvent?.Invoke(weaponInfoData);
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