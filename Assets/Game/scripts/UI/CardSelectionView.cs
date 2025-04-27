using DG.Tweening;
using UnityEngine;

public class CardSelectionView : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject unselectedObj;
    [SerializeField] private GameObject selectedObj;
    [SerializeField] private ParticleSystem vfxSelection;
        public void Select()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0.8f, 0.25f).SetEase(Ease.Linear));
        sequence.Append(transform.DOScale(1.2f, 0.35f).SetEase(Ease.OutBack));
        selectedObj.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0.8f, 0.25f).SetEase(Ease.Linear));
        vfxSelection.gameObject.SetActive(false);
        unselectedObj.SetActive(true);
    }
}