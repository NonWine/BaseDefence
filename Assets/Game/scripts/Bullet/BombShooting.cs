using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BombShooting : MonoBehaviour
{
    [Inject] private BulletFactory bulletFactory;
    [SerializeField] DynamicWeaponHandler unlockedWeapon;
    private void BombShoot()
    {
        var bullet = bulletFactory.Create(unlockedWeapon.weaponInfoData.baseBullet.GetType());
    }
}
