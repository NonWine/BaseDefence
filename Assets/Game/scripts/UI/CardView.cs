using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button button;

    public event Action<WeaponInfoData> OnClickedWeaponEvent; 
    
    private WeaponInfoData weaponInfoData;

    private void Awake()
    {
        button.onClick.AddListener(ClickEvent);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(ClickEvent);
    }

    public void SetData(WeaponInfoData weaponInfoData)
    {
        this.weaponInfoData = weaponInfoData;
        icon.sprite = weaponInfoData.image;
        title.text = this.weaponInfoData.WeaponName;
        description.text = this.weaponInfoData.description;
    }

    public void DestroyCard()
    {
        transform.DOScale(0f, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void ClickEvent()
    {
        OnClickedWeaponEvent?.Invoke(weaponInfoData);
    }
}
