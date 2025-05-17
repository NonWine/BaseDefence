using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private LayoutElement _layoutElement;
    protected WindowsController WindowsController;
    [field: SerializeField] public string WindowName { get; private set; }
    private RectTransform _rectTransform;

    public event Action OnOpenWindow;
    
    public event Action OnCloseWindow;

    public event Action OnUpdateWindow;

    public virtual void Initialize(WindowsController windowsController)
    {
        _layoutElement = GetComponent<LayoutElement>();
        _rectTransform = GetComponent<RectTransform>();
        WindowsController = windowsController;
    }
    
    
    

    public virtual void OpenWindow()
    {
        OnOpenWindow?.Invoke();
        gameObject.SetActive(true);
        Debug.Log("Open Window" + WindowName);

        
        _layoutElement.ignoreLayout = true;
        _rectTransform.anchoredPosition = new Vector2(Screen.width, _rectTransform.anchoredPosition.y);
        _rectTransform.DOAnchorPos(new Vector2(0f, _rectTransform.anchoredPosition.y), 0.35f).SetEase(Ease.OutBack).OnComplete(
            () =>
            {
                _layoutElement.ignoreLayout = false;
                _rectTransform.SetAsFirstSibling();
            });
        
    }

    public virtual void CloseWindow()
    {
        gameObject.SetActive(false);
        OnCloseWindow?.Invoke();

        Debug.Log("Close Window" + WindowName);

    }

    public virtual void Tick()
    {
        OnUpdateWindow?.Invoke();
    }
    
}

