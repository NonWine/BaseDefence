using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Linq;

public class DoubleLaser : BaseBullet
{
    [Inject] private EnemyFactory enemyFactory;
    [Inject] private GameManager gameManager;
    [Inject] protected DiContainer diContainer;
    [SerializeField] private SimpleLaserRay simpleLaser;
    public override Type Type => typeof(DoubleLaser);
    public override void Init(Transform target)
    {
        base.Init(target);
        if (target != null)
        {
            var firstEnemies = enemyFactory.Enemies
            .Where(e => !e.IsDeath && Vector3.Distance(transform.position, e.transform.position) < 100)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position));
            if(firstEnemies.Count() > 0)
            {
                if(firstEnemies.Count() > 1)
                {
                    SimpleLaserRay laser = diContainer.InstantiatePrefabForComponent<SimpleLaserRay>(simpleLaser, transform.position, Quaternion.identity, null);
                    laser.transform.GetComponent<SimpleLaserRay>().RayShoot(firstEnemies.ElementAt(0).transform, WeaponUpgradeData);
                    SimpleLaserRay laser1 = diContainer.InstantiatePrefabForComponent<SimpleLaserRay>(simpleLaser, transform.position, Quaternion.identity, null);
                    laser1.transform.GetComponent<SimpleLaserRay>().RayShoot(firstEnemies.ElementAt(1).transform, WeaponUpgradeData);

                }
                else if(firstEnemies.Count() == 1)
                {
                    SimpleLaserRay laser = diContainer.InstantiatePrefabForComponent<SimpleLaserRay>(simpleLaser, transform.position, Quaternion.identity, null);
                    laser.transform.GetComponent<SimpleLaserRay>().RayShoot(firstEnemies.ElementAt(0).transform, WeaponUpgradeData);
                    SimpleLaserRay laser1 = diContainer.InstantiatePrefabForComponent<SimpleLaserRay>(simpleLaser, transform.position, Quaternion.identity, null);
                    laser1.transform.GetComponent<SimpleLaserRay>().RayShoot(firstEnemies.ElementAt(0).transform, WeaponUpgradeData);
                }
                else
                {
                    
                }

            }
            /*LaserRay laser = diContainer.InstantiatePrefabForComponent<LaserRay>(_laser, transform.position, Quaternion.identity, null);
            laser.transform.GetComponent<LaserRay>().RayShoot(target, WeaponUpgradeData);*/
        }
        DestroyBullet();
    }
}
