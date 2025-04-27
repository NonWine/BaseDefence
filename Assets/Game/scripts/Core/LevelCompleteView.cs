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
    [SerializeField] private float fadeDuration = 0.25f;
    [Inject] private WeaponManagerView weaponManagerView;

    private void Awake()
    {
        weaponManagerView.OnGetWeaponEvent += Hide;
        
    }

    private void OnDestroy()
    {
        weaponManagerView.OnGetWeaponEvent -= Hide;

    }

    [Button]
    public void Show() => StartCoroutine(ShowCor());

    public void Hide(WeaponInfoData weaponInfoData) => StartCoroutine(HideCor());
    
    private IEnumerator ShowCor()
    {

        winPanel.DOFade(1f, fadeDuration).SetEase(Ease.OutBack);
        winPanel.interactable = true;
        //yield return new WaitForSeconds(fadeDuration);
        weaponManagerView.CreateCards();
        yield break;
    }


    private IEnumerator HideCor()
    {
        yield return new WaitForSeconds(1f);
        weaponManagerView.DestroyCards();
        yield return new WaitForSeconds(0.2f);
        winPanel.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        winPanel.interactable = false;
        Debug.Log("Hide Level Complete Canvas");
    }
}