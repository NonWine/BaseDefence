using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class WeaponManagerView : MonoBehaviour
{
    [SerializeField] private WeaponInfoData[] allWeapons;
    [SerializeField] private CardView cardViewPrefab;
    [SerializeField] private GameObject cardContainer;
    [Inject] private DiContainer diContainer;
    [Inject] private GameManager gameManager;
    
    [ShowInInspector, ReadOnly]  public List<CardView> cardViews { get; private set; } = new List<CardView>() ;

    public event Action<WeaponInfoData> OnGetWeaponEvent;

    private void Awake()
    {
        gameManager.OnLevelCompleteEvent += CreateCards;
    }

    private void OnDestroy()
    {
        gameManager.OnLevelCompleteEvent -= CreateCards;
    }

    public void CreateCards()
    {
        for (int i = 0; i < 3; i++)
        {
            var card = diContainer.InstantiatePrefabForComponent<CardView>(cardViewPrefab,cardContainer.transform);
            cardViews.Add(card);
            card.OnClickedWeaponEvent += GetWeapon;
        }
        
    }

    private void GetWeapon(WeaponInfoData weaponInfoData)
    {
        for (var i = cardViews.Count - 1; i >= 0; i--)
        {
            cardViews[i].OnClickedWeaponEvent -= GetWeapon; 
        }
        
        OnGetWeaponEvent?.Invoke(weaponInfoData);
    }
}