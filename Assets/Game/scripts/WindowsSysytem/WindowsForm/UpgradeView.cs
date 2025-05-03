using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] protected Image icon;
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected Button button;
    [SerializeField] private TMP_Text levelTextCurrent;
    [SerializeField] private TMP_Text levelTextNext;

    public void Show()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }
    
    
}