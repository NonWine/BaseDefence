using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class WeaponCardPanelView : MonoBehaviour
{
    [Inject] private WeaponManagerView weaponManagerView;

    public event Action OnClickCardEvent;
    
    private void Awake()
    {
        weaponManagerView.OnGetWeaponEvent += HidePanel;
    }

    private void OnDestroy()
    {
        weaponManagerView.OnGetWeaponEvent -= HidePanel;
    }


    public void HidePanel(WeaponInfoData weaponInfoData)
    {
        StartCoroutine(HideViewCor());
    }

    public void ShowPanel()
    {
        StartCoroutine(ShowViewCor());
    }
    
    private IEnumerator ShowViewCor()
    {
        weaponManagerView.CreateCards();
        yield break;
    }

    private IEnumerator HideViewCor()
    {
        for (var i = weaponManagerView.cardViews.Count - 1; i >= 0; i--)
        {
            weaponManagerView.cardViews[i].DestroyCard();
            weaponManagerView.cardViews.RemoveAt(i);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.15f);
        OnClickCardEvent?.Invoke();
        yield break;
    }

}