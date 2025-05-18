using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvolkaController : StaticWeaponController
{
    [SerializeField] private Provolka provolka;
    [SerializeField] private Transform provolkaPoint;
    private void Start()
    {
        UnlockCallback += CreateProvolka;
    }
    private void CreateProvolka()
    {
        diContainer.InstantiatePrefabForComponent<Provolka>(provolka, provolkaPoint.position, Quaternion.identity, null).
            Init(WeaponInfoData);
    }
    protected override void UnLockedUpdate()
    {

    }
}
