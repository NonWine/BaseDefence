using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WaveLevelCompleteView : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCanvas;
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private LevelRewardView levelRewardView;
    [SerializeField] private Button rewardButton;
    [Inject] private CollectableManager collectableManager;
    private LevelData currentLevelData;

    private void Awake()
    {
        rewardButton.onClick.AddListener(Hide);
    }

    private void OnDestroy()
    {
        rewardButton.onClick.RemoveListener(Hide);

    }

    [Button]
    public void Show(LevelData levelData)
    {
        currentLevelData = levelData;
        levelRewardView.CreateRewards(levelData);
        StartCoroutine(ShowCor());
    } 
    
    [Button]
    public void Hide() => StartCoroutine(HideCor());
    
    private IEnumerator ShowCor()
    {
        mainCanvas.alpha = 0f;
        mainCanvas.DOFade(1f, fadeDuration).SetEase(Ease.OutBack).SetUpdate(true);
        mainCanvas.interactable = true;
        yield break;
    }


    private IEnumerator HideCor()
    {
        levelRewardView.DestroyRewards();
        foreach (var rewardContainer in currentLevelData.RewardsContainer)
        {
            collectableManager.GetWallet(rewardContainer.Type.TypeWallet).Add(rewardContainer.Count);
        }
        mainCanvas.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete( ()=>
        {
        }).SetUpdate(true);;
        mainCanvas.interactable = false;
        
        
        Debug.Log("Hide Level Complete Canvas");
        yield break;
    }
}