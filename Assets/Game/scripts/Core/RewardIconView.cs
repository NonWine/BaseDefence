using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardIconView : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TMP_Text count;

    public void Initialize(Sprite icon, int rewardCount)
    {
        count.text = rewardCount.ToString();
        _sprite.sprite = icon;
    }
}