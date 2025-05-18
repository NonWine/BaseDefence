using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BombShooting : StaticWeaponController
{
    [Inject] private BulletFactory bulletFactory;
    [SerializeField] DynamicWeaponHandler unlockedWeapon;
    [SerializeField] WeaponUpgradeData weaponUpgradeData;
    [SerializeField] Transform bombStartPos;
    [SerializeField] Transform bombTargetPos;
    [SerializeField] Button bombButton;
    private float colDown;
    private float timer;

    private void Start()
    {
        if(weaponUpgradeData != null)
            colDown = weaponUpgradeData.GetStat(StatName.CoolDown).CurrentValue;
        else
            colDown = 5f;
        timer = colDown;
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
        var bullet = bulletFactory.Create(unlockedWeapon.weaponInfoData.baseBullet.GetType());
        bullet.Init(bombTargetPos);
        bullet.transform.position = bombStartPos.position;
        timer = colDown;
        bombButton.interactable = false;
    }
    private void Update()
    {
        if(timer == 0)
        {
            if(bombButton.interactable == false)
            {
                bombButton.interactable = true;
            }
            return;
        }
        timer -= Time.deltaTime;
    }
    protected override void UnLockedUpdate()
    {

    }
}
