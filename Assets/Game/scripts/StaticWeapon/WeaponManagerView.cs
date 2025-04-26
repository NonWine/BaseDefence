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
    
    [ShowInInspector, ReadOnly]  public List<CardView> cardViews { get; private set; } = new List<CardView>() ;

    public event Action<WeaponInfoData> OnGetWeaponEvent;
    

    public void CreateCards()
    {
        for (int i = 0; i < 3; i++)
        {
            var card = diContainer.InstantiatePrefabForComponent<CardView>(cardViewPrefab,cardContainer.transform);
            card.SetData(allWeapons[i]);
            card.OnClickedWeaponEvent += GetWeapon;
            cardViews.Add(card);
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