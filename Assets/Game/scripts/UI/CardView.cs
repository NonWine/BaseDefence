using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardView : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected Button button;
    
    private void Awake()
    {
        button.onClick.AddListener(ClickEvent);
    }

    public abstract void SetData(WeaponInfoData weaponInfoData);

    private void OnDestroy()
    {
        button.onClick.RemoveListener(ClickEvent);
    }

    protected abstract void ClickEvent();
}