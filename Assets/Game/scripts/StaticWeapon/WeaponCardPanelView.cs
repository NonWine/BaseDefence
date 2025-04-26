using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class WeaponCardPanelView : MonoBehaviour
{
    [SerializeField] private GameObject PanelView;
    [Inject] private WeaponManagerView weaponManagerView;
    [Inject] private GameManager gameManager;
    
    private void Awake()
    {
        gameManager.OnLevelCompleteEvent += ShowPanel;
        weaponManagerView.OnGetWeaponEvent += HidePanel;
    }

    private void OnDestroy()
    {
        gameManager.OnLevelCompleteEvent -= ShowPanel;
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
        PanelView.gameObject.SetActive(true);
        PanelView.transform.localScale = Vector3.zero;
        PanelView.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);


        yield return new WaitForSeconds(0.2f);

        weaponManagerView.CreateCards();
    }

    private IEnumerator HideViewCor()
    {
        for (var i = weaponManagerView.cardViews.Count - 1; i >= 0; i--)
        {
            weaponManagerView.cardViews[i].DestroyCard();
            weaponManagerView.cardViews.RemoveAt(i);
        }

        yield return new WaitForSeconds(0.2f);

        PanelView.transform.DOScale(0f, 0.25f).OnComplete(() =>
        {
            PanelView.gameObject.SetActive(false);
        });
    }

}