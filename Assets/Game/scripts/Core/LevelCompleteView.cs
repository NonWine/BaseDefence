using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class LevelCompleteView : MonoBehaviour
{
    [SerializeField] private CanvasGroup winPanel;
    [SerializeField] private CanvasGroup mainCanvas;
    [SerializeField] private float fadeDuration = 0.25f;
    [Inject] private WeaponCardManagerView _weaponCardManagerView;

    private void Awake()
    {
        PlayerLevelController.OnLevelUp += Show;
        _weaponCardManagerView.OnGetWeaponEvent += Hide;
        
    }

    private void OnDestroy()
    {
        PlayerLevelController.OnLevelUp -= Show;
        _weaponCardManagerView.OnGetWeaponEvent -= Hide;

    }

    [Button]
    public void Show() => StartCoroutine(ShowCor());

    public void Hide(WeaponInfoData weaponInfoData) => StartCoroutine(HideCor());
    
    private IEnumerator ShowCor()
    {
        mainCanvas.alpha = 0f;
        Time.timeScale = 0f;
        winPanel.DOFade(1f, fadeDuration).SetEase(Ease.OutBack).SetUpdate(true);
        ;
        winPanel.interactable = true;
        //yield return new WaitForSeconds(fadeDuration);
        _weaponCardManagerView.CreateCards();
        yield break;
    }


    private IEnumerator HideCor()
    {
        yield return new WaitForSecondsRealtime(1f);
        _weaponCardManagerView.DestroyCards();
        yield return new WaitForSecondsRealtime(0.2f);
        winPanel.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete( ()=>
        {
            mainCanvas.alpha = 1f;
            Time.timeScale = 1f;
        }).SetUpdate(true);;
        winPanel.interactable = false;
        
        
        Debug.Log("Hide Level Complete Canvas");
    }
}