using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableVisualPart : MonoBehaviour
{
    private CollectableAnimationData.SendingData _animData => CollectableAnimationData.SendingAnimationData;
    [SerializeField, ReadOnly] private RectTransform _rectTransform;
    [SerializeField, ReadOnly] private Transform _parent;
    public CollectableAnimationData CollectableAnimationData;
    private Sequence _animation;

    public event Action onEndSending;

    #region Editor
    private void OnValidate() 
        => SetRefs();

    [Button]
    private void SetRefs()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parent = transform.parent;
    }
    #endregion

    public void Initialize(RectTransform startTransform)
    {
        transform.position = startTransform.position;
        Initialize();
    }
    
    public void Initialize(Transform startTransform)
    {
        transform.position = startTransform.position;
        Initialize();
    }

    public void Initialize(Vector3 startTransform)
    {
        transform.position = startTransform;
        Initialize();
    }
    
    private void Initialize()
    {
        _animation?.Kill();
        _rectTransform.DOKill();
        
        _rectTransform.localRotation = Quaternion.identity;
        _rectTransform.localScale = Vector3.one * 1f;
    }

    public void MoveTo(RectTransform target, CollectableWallet wallet, int value)
    {
        gameObject.SetActive(true);
        _animation?.Kill();

        _animation = DOTween.Sequence();
        _rectTransform.localScale = Vector3.zero;
        
        _animation.AppendInterval(Random.Range(_animData.RandomRange.Min,_animData.RandomRange.Max));
        _animation.Append(_rectTransform.DOScale(_animData.FirstStage.ScaleFactor, 0.35f)).SetEase(Ease.OutBack);
        _animation.AppendInterval(Random.Range(0f,0.2f));
        _animation.Append(_rectTransform.DOMove(target.position, _animData.ThirdStage.Time).SetEase(Ease.OutQuad));
        _animation.Join(_rectTransform.DOScale(0f, _animData.ThirdStage.Time + _animData.SecondStage.Time)).SetEase(Ease.Linear);

        _animation.OnComplete(() =>
        {
            onEndSending?.Invoke();
            transform.SetParent(_parent);
            gameObject.SetActive(false);
        });
    }

    private Vector2 GetOffset()
    {
        float angle = Random.Range(0, 360f);
        float radius = Random.Range(_animData.Radius * (1f - _animData.RadiusThickness), _animData.Radius);

        return (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right * radius) + _animData.FirstStage.Offset;
    }
}
