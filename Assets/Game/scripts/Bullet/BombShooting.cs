using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BombShooting : StaticWeaponController
{
    [Inject] private BulletFactory bulletFactory;
    [SerializeField] private BombBullet BombBullet;
    [SerializeField] Transform bombStartPos;
    [SerializeField] Transform bombTargetPos;
    [SerializeField] Button bombButton;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private WeaponCardManagerView weaponCardManager;
    [SerializeField] private Image filledImage;
    [SerializeField] private List<Image> bombCounts;
    private List<Image> currentBombCounts;
    private int count;
    private float colDown;
    private float timerColDown;

    private void Start()
    {
        waveManager.OnLevelFinished += ResetBombCount;
        weaponCardManager.OnGetWeaponEvent += UpdateCounts;
        colDown = WeaponInfoData.WeaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue;
        UnlockCallback += ShowBomb;
        timerColDown = colDown;
    }
    
    private void ShowBomb()
    {
        
        bombButton.gameObject.SetActive(true);
        bombButton.onClick.AddListener(BombShoot);
        currentBombCounts = new List<Image>();
        isLocked = false;
        for (var i = 0; i <WeaponInfoData.WeaponUpgradeData.GetStat(StatName.ZaryadCount).CurrentValueInt; i++)
        {
            currentBombCounts.Add(bombCounts[i]);
            currentBombCounts[i].transform.parent.gameObject.SetActive(true);
        }
    }

    private void ResetBombCount()
    {
        count = 0;
        foreach (var bombCount in bombCounts)
        {
            bombCount.gameObject.SetActive(true);
        }

        filledImage.fillAmount = 1f;
        bombButton.interactable = true;
    }

    private void UpdateCounts(WeaponInfoData weapon)
    {
        if (weapon.WeaponName == WeaponInfoData.WeaponName)
        {
            currentBombCounts = new List<Image>();
            for (var i = 0; i <WeaponInfoData.WeaponUpgradeData.GetStat(StatName.ZaryadCount).CurrentValueInt; i++)
            {
                currentBombCounts.Add(bombCounts[i]);
                currentBombCounts[i].transform.parent.gameObject.SetActive(true);
            }
        }
    }
    
    private void OnDestroy()
    {
        waveManager.OnLevelFinished -= ResetBombCount;
        bombButton.onClick.RemoveAllListeners();
        weaponCardManager.OnGetWeaponEvent -= UpdateCounts;

    }
    
    private void BombShoot()
    {
        currentBombCounts[count].gameObject.SetActive(false);
        count++;
        
        
        var bullet = bulletFactory.Create(BombBullet.GetType());
        bullet.transform.position = bombStartPos.position;
        bullet.Init(bombTargetPos);
        timerColDown = colDown;
        bombButton.interactable = false;
        TurnOnButton();
    }

    private void TurnOnButton()
    {
        if (count >= WeaponInfoData.WeaponUpgradeData.GetStat(StatName.ZaryadCount).CurrentValue)
        {
            Debug.Log(count);
            bombButton.interactable = false;
            return;
        }
        filledImage.fillAmount = 0f;
        filledImage.DOFillAmount(1f, colDown).OnComplete(() =>
        {
            bombButton.interactable = true;

        }).SetEase(Ease.Linear);
    }
    
    protected override void UnLockedUpdate()
    {

    }
}
