﻿using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class PlayerLevelCompleteView : MonoBehaviour
{
    [SerializeField] private CanvasGroup winPanel;
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private GiveStartWeapon GiveStartWeapon;
    [Inject] private WeaponCardManagerView _weaponCardManagerView;
    [Inject] private GameManager GameManager;
    [Inject] private PlayerLevelController playerLevelController;
    private string playerChoseWeaponPlayerPref;
    
    private void Awake()
    {
        /*if(PlayerPrefs.GetInt(playerChoseWeaponPlayerPref, 0) == 1)
        {
            return;
        }
        PlayerPrefs.SetInt(playerChoseWeaponPlayerPref, 1);*/
        playerLevelController.OnLevelUp += Show;
        _weaponCardManagerView.OnGetWeaponEvent += Hide;
        //
        ShowStatView();
    }

    private void OnDestroy()
    {
        playerLevelController.OnLevelUp = null;
        _weaponCardManagerView.OnGetWeaponEvent -= Hide;

    }

    [Button]
    public void Show()
    {
        if(!_weaponCardManagerView.CanCreateCards) return;
        Debug.Log("12123");
        winPanel.DOFade(1f, fadeDuration).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            GameManager.TimeGameSpeed = 0;
            winPanel.blocksRaycasts = true;
            _weaponCardManagerView.CreateCards();

        });
    }

    public void ShowStatView()
    {
        Debug.Log("12123");
        winPanel.DOFade(1f, fadeDuration).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            GameManager.TimeGameSpeed = 0;
            winPanel.blocksRaycasts = true;
            GiveStartWeapon.CreateStartWeapon();
        });
    }

    public void Hide(WeaponInfoData weaponInfoData) => StartCoroutine(HideCor());
    


    private IEnumerator HideCor()
    {
        yield return new WaitForSecondsRealtime(1f);
        _weaponCardManagerView.DestroyCards();
        yield return new WaitForSecondsRealtime(0.2f);
        winPanel.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete( ()=>
        {
            GameManager.TimeGameSpeed = 1;
        }).SetUpdate(true);;
        winPanel.blocksRaycasts = false;
        
        
        Debug.Log("Hide Level Complete Canvas");
    }
}