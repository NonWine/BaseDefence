using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelCompleteView : MonoBehaviour
{
    [SerializeField] private CanvasGroup winPanel;
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private WeaponCardPanelView weaponCardPanelView;

    private void Awake()
    {
        weaponCardPanelView.OnClickCardEvent += Hide;
        
    }

    private void OnDestroy()
    {
        weaponCardPanelView.OnClickCardEvent -= Hide;

    }

    [Button]
    public void Show() => StartCoroutine(ShowCor());

    private IEnumerator ShowCor()
    {

        winPanel.DOFade(1f, fadeDuration).SetEase(Ease.OutBack);
        winPanel.interactable = true;
        yield return new WaitForSeconds(fadeDuration);
        yield return new WaitForSeconds(0.5f);
        weaponCardPanelView.ShowPanel();
    }
    
    [Button]
    public void Hide()
    {
        winPanel.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        winPanel.interactable = false;
        Debug.Log("Hide Level Complete Canvas");
    }
}