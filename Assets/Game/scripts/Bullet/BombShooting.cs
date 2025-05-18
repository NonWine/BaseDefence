using System.Collections;
using System.Collections.Generic;
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
    private float colDown;
    private float timerColDown;

    private void Start()
    {
        colDown = WeaponInfoData.WeaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue;
        timerColDown = colDown;
    }
    
    private void OnEnable()
    {
        bombButton.onClick.AddListener(BombShoot);
    }
    
    private void OnDisable()
    {
        bombButton.onClick.RemoveAllListeners();
    }
    
    private void BombShoot()
    {
        isLocked = false;
        var bullet = bulletFactory.Create(BombBullet.GetType());
        bullet.transform.position = bombStartPos.position;
        bullet.Init(bombTargetPos);
        timerColDown = colDown;
        bombButton.interactable = false;
        Invoke(nameof(TurnOnButton), colDown);
    }

    private void TurnOnButton()
    {
        bombButton.interactable = true;

    }
    
    protected override void UnLockedUpdate()
    {

    }
}
