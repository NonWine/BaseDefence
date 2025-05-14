using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class BombBullet : BaseBullet
{
    [Inject] private GameManager gameManager;

    public override Type Type => typeof(BombBullet);

    public override void Init(Transform target)
    {
        base.Init(target);
        target = gameManager.BombTarget;
    }


}
